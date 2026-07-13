using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoryFlow.Exceptions;
using StoryFlow.Helpers;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;
using System.Text;
using System.Text.Json;

namespace StoryFlow.Services
{
    public class StoryService : IStoryService
    {
        private readonly IAggregateStoryRepository _storyRepository;
        private readonly IAggregateStoryValidator _serviceValidator;
        private readonly ISentenceBuilder _sentenceBuilder;
        private readonly ITextConverter _textConverter;
        private readonly ITextCounter _textCounter;
        private readonly IMapper _mapper;
        private readonly GeminiSchemaGenerator _geminiSchemaGenerator;
        private readonly IAggregateUserRepository _userRepository;
        private readonly IUserStoryRepository _userStoryRepository;
        private readonly GeminiSettings _geminiSettings;

        public StoryService(IAggregateStoryRepository storyRepository, IAggregateStoryValidator serviceValidator, ISentenceBuilder sentenceBuilder, ITextConverter textConverter, ITextCounter textCounter, IMapper mapper, IOptions<GeminiSettings> geminiSettings, GeminiSchemaGenerator geminiSchemaGenerator, IAggregateUserRepository userRepository, IUserStoryRepository userStoryRepository)
        {
            _storyRepository = storyRepository;
            _serviceValidator = serviceValidator;
            _sentenceBuilder = sentenceBuilder;
            _textConverter = textConverter;
            _textCounter = textCounter;
            _mapper = mapper;
            _geminiSchemaGenerator = geminiSchemaGenerator;
            _userRepository = userRepository;
            _userStoryRepository = userStoryRepository;
            _geminiSettings = geminiSettings.Value;
        }
        public async Task Add(AddStoryDto item)
        {
            _serviceValidator.Validate(item);

            Story story = _mapper.Map<Story>(item);

            story.StorySize = _textCounter.SetTextSize(item.EnglishStory!);

            var englishSentences = _textConverter.GetSentencesFromText(item.EnglishStory!);
            var polishSentences = _textConverter.GetSentencesFromText(item.PolishStory!);

            _serviceValidator.ValidateSentencesCount(polishSentences.Count, englishSentences.Count);

            _sentenceBuilder.AddSentencesToStory(story, polishSentences, englishSentences);

            await _storyRepository.Add(story);
        }

        public async Task<string> Generate(GenerateStoryDto dto)
        {
            string storyLengthMin = "";
            string storyLengthMax = "";

            object schema = _geminiSchemaGenerator.GenerateStorySchema();

            if (dto.StorySize == StorySize.Short)
            {
                storyLengthMin = "400";
                storyLengthMax = "699";
            }

            if (dto.StorySize == StorySize.Medium)
            {
                storyLengthMin = "700";
                storyLengthMax = "999";
            }

            if (dto.StorySize == StorySize.Long)
            {
                storyLengthMin = "1000";
                storyLengthMax = "1500";
            }

            if (dto.StorySize != StorySize.Short && dto.StorySize != StorySize.Medium && dto.StorySize != StorySize.Long)
            {
                throw new BadRequestException("Historia w języku angielskim musi mieć od 400 do 1500 znaków.");
            }

            var additionalInstructions = string.IsNullOrWhiteSpace(dto.AdditionalInstructions)
                ? ""
                : $""" 
        Additional instructions:
        - {dto.AdditionalInstructions}
        """;


            using var client = new HttpClient
            {
                BaseAddress = new Uri(_geminiSettings.BaseUrl)
            };


            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = $"""
                        Create one story in English and provide a Polish translation.

                        Requirements:
                        - Category: {dto.StoryCategory}
                        - Language level: {dto.LanguageLevel}
                        - Title length in both languages: 5 - 50 characters
                        - Description length in both languages: 10 - 100 characters
                        - English story length: from {storyLengthMin} to {storyLengthMax} characters
                        - The number of sentences in both versions must be exactly the same.
                        - Keep the same sentence order and meaning in both versions.
                        - The Polish version should be a natural translation, not a word-for-word translation.

                        Rules:
                        - Check the length before finishing the story.
                        - Do not exceed the character limit.
                        - Adapt the writing style to the language level.

                        {additionalInstructions}

                        Return ONLY JSON that matches the responseSchema.
                        """
                    }
                }
            }
        },
                generationConfig = new
                {
                    responseMimeType = "application/json",
                    responseSchema = schema
                }
            };


            var json = JsonSerializer.Serialize(requestBody);

            var response = await client.PostAsync(
                $"/v1beta/models/{_geminiSettings.ModelName}:generateContent?key={_geminiSettings.ApiKey}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            var geminiResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine(geminiResponse);

            return geminiResponse;
        }

        public async Task<List<GetStorySeasonDto>> GetSeasons(string? userIdClaim)
        {
            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedException("Użytkownik nie ma uprawnień do wykonania tej operacji.");
            }

            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            List<StorySeason> seasons = await _storyRepository.GetStorySeasonsAsync();

            List<StorySeason> seasonsVisibleForUser = seasons
                .Where(s => s.IsVisibleForUser)
                .ToList();

            foreach (var season in seasonsVisibleForUser)
            {
                season.MaxPoints = season.Stories.Sum(s => s.StoryPoint?.MaxPoints ?? 0);
            }

            List<GetStorySeasonDto> dto = _mapper.Map<List<GetStorySeasonDto>>(seasonsVisibleForUser);
            
            return dto;
        }

        public async Task<ICollection<GetStoryDto>> Get(StoryFilter? filter, int userId)
        {
            IQueryable<Story> query = _storyRepository.Get();

            if (filter != null)
            {
                if (filter.LanguageLevel.HasValue)
                    query = query.Where(x => x.LanguageLevel == filter.LanguageLevel.Value);

                if (filter.Category.HasValue)
                    query = query.Where(x => x.StoryCategory == filter.Category.Value);

                if (filter.Size.HasValue)
                    query = query.Where(x => x.StorySize == filter.Size.Value);
            }

            List<Story>? results = await query.ToListAsync();

            List<GetStoryDto> stories = _mapper.Map<List<GetStoryDto>>(results);

            foreach (var story in stories)
            {
                var userStory = story.UserStories
                    .FirstOrDefault(us => us.UserId == userId);

                if (userStory != null)
                {
                    story.UserStories = new List<GetUserStoryDto> { userStory };
                }
                else
                {
                    story.UserStories = new List<GetUserStoryDto>();
                }
            }

            return stories;
        }
        public async Task<List<GetStoryDto>> GetBySeason(int seasonId, string? userIdClaim)
        {
            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedException("Użytkownik nie ma uprawnień do wykonania tej operacji.");
            }

            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            _serviceValidator.ValidateId(seasonId);

            if (seasonId == 2)
            {
                if (user.Stars < 750)
                {
                    throw new BadRequestException("Nie masz wystarczającej ilości gwiazdek, by przejść do tego sezonu.");
                }
            }

            if (seasonId == 3)
            {
                if (user.Stars < 1000)
                {
                    throw new BadRequestException("Nie masz wystarczającej ilości gwiazdek, by przejść do tego sezonu.");
                }
            }


            IQueryable<Story> query = _storyRepository.GetBySeason(seasonId);

            List<Story>? stories = await query.ToListAsync();

            List<GetStoryDto> dto = _mapper.Map<List<GetStoryDto>>(stories);

            return dto;
        }


        public async Task<GetStoryDto> Get(int storyId, string? userIdClaim)
        {
            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedException("Użytkownik nie ma uprawnień do wykonania tej operacji.");
            }

            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            _serviceValidator.ValidateId(storyId);

            Story? story = await _storyRepository.Get(storyId);

            if (story == null)
            {
                throw new NotFoundException("Nie znaleziono historii.");
            }

            UserStory? userStory = await _userStoryRepository.GetByUserAndStory(user.Id, story.Id);

            if (user.Tickets < 1 && userStory == null)
            {
                throw new BadRequestException("Nie masz już żadnych biletów. Ukończ kolejny otwarty quiz z wynikiem co najmniej 50%, aby zdobyć kolejny.");
            }

            if (user.Tickets >= 1 && userStory == null)
            {
                var userStoryItem = new UserStory
                {
                    UserId = user.Id,
                    StoryId = story.Id
                };
                await _userStoryRepository.Add(userStoryItem);
                user.Tickets--;
                await _userRepository.Update(user);
            }

            var storyDto = _mapper.Map<GetStoryDto>(story);

            return storyDto;
        }

        public async Task Remove(int id)
        {
            _serviceValidator.ValidateId(id);

            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            await _storyRepository.Remove(result!);
        }
    }
}
