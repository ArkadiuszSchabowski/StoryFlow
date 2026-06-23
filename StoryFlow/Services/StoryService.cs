using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoryFlow.Exceptions;
using StoryFlow.Helpers;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
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
        private readonly IPointsCalculator _pointsCalculator;
        private readonly GeminiSettings _geminiSettings;

        public StoryService(IAggregateStoryRepository storyRepository, IAggregateStoryValidator serviceValidator, ISentenceBuilder sentenceBuilder, ITextConverter textConverter, ITextCounter textCounter, IMapper mapper, IOptions<GeminiSettings> geminiSettings, GeminiSchemaGenerator geminiSchemaGenerator, IPointsCalculator pointsCalculator)
        {
            _storyRepository = storyRepository;
            _serviceValidator = serviceValidator;
            _sentenceBuilder = sentenceBuilder;
            _textConverter = textConverter;
            _textCounter = textCounter;
            _mapper = mapper;
            _geminiSchemaGenerator = geminiSchemaGenerator;
            _pointsCalculator = pointsCalculator;
            _geminiSettings = geminiSettings.Value;
        }
        public async Task Add(AddStoryDto item)
        {
            _serviceValidator.Validate(item);

            Story story = _mapper.Map<Story>(item);

            story.StorySize = _textCounter.SetTextSize(item.EnglishStory!);
            story.MaxPoints = _pointsCalculator.SetMaxPoints(story.StorySize, story.LanguageLevel);

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
                    Write a story in English and Polish.
                    
                    Requirements:
                    - Category: {dto.StoryCategory}
                    - Language level: {dto.LanguageLevel}
                    - Length: from {storyLengthMin} to {storyLengthMax} characters
                    
                    Rules:
                    - Check the length before finishing the story
                    - Do not exceed the character limit
                    - Adapt the writing style to the language level

                    Quiz requirements:
                    - Generate exactly 4 quiz questions based on the story
                    - Each question must have exactly 4 answer options
                    - Only one answer can be correct
                    - Use English for questions and answers
                    - Return the index of the correct answer (0-3)
                    
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
            return geminiResponse;
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

        public async Task<GetStoryDto> Get(int id)
        {
            _serviceValidator.ValidateId(id);

            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            var storyDto = _mapper.Map<GetStoryDto>(result);

            return storyDto;
        }

        public async Task Remove(int id)
        {
            _serviceValidator.ValidateId(id);

            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            await _storyRepository.Remove(result!);
        }

        public async Task SaveStoryBestResultForUser(int userId, int storyId, int result)
        {
            var story = await _storyRepository.Get(storyId);

            if(story == null)
            {
                throw new BadRequestException("Story not found.");
            }

            _serviceValidator.ValidateResult(story.StorySize, story.LanguageLevel, result);

            UserStory? userStory = await _storyRepository.GetByUserAndStory(userId, storyId);

            if (userStory == null)
            {
                userStory = new UserStory
                {
                    StoryId = storyId,
                    UserId = userId,
                    BestResult = result
                };

                await _storyRepository.AddBestResult(userStory);
                return;
            }

            if (result > userStory.BestResult)
            {
                userStory.BestResult = result;
                await _storyRepository.Update(userStory);
            }
        }
    }
}
