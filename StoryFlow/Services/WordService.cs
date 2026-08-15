using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class WordService : IWordService
    {
        private readonly IAggregateUserRepository _userRepository;
        private readonly IAggregateWordRepository _wordRepository;
        private readonly IAggregateStoryValidator _serviceValidator;
        private readonly IMapper _mapper;
        private readonly IUserWordLessonRepository _userWordLessonRepository;

        public WordService(IAggregateUserRepository userRepository, IAggregateWordRepository wordRepository, IAggregateStoryValidator serviceValidator, IMapper mapper, IUserWordLessonRepository userWordLessonRepository)
        {
            _userRepository = userRepository;
            _wordRepository = wordRepository;
            _serviceValidator = serviceValidator;
            _mapper = mapper;
            _userWordLessonRepository = userWordLessonRepository;
        }

        public async Task<GetWordLessonDto> Get(int wordLessonId, string? userIdClaim)
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

            _serviceValidator.ValidateId(wordLessonId);

            WordLesson? wordLesson = await _wordRepository.Get(wordLessonId);

            if (wordLesson == null)
            {
                throw new NotFoundException("Nie znaleziono lekcji.");
            }

            var storySeason = _wordRepository.GetBySeason(userId, wordLesson.StorySeasonId);

            var result = await storySeason.FirstOrDefaultAsync();

            if (result != null)
            {
                if (result.StorySeason != null)
                {
                    if (user.Stars < result.StorySeason.PointsRequiredToUnlock)
                    {
                        throw new BadRequestException("Nie masz wystarczającej ilości gwiazdek, by podejrzeć tą lekcję.");
                    }
                }
            }

            UserWordLesson? userWordLesson = await _userWordLessonRepository.GetByUserAndWordLesson(user.Id, wordLesson.Id);

            if (user.Tickets < 1 && userWordLesson == null)
            {
                throw new BadRequestException("Nie masz już żadnych biletów. Ukończ kolejny otwarty quiz z wynikiem co najmniej 50%, aby zdobyć kolejny.");
            }

            if (user.Tickets >= 1 && userWordLesson == null)
            {
                var userWordLessonItem = new UserWordLesson
                {
                    UserId = user.Id,
                    WordLessonId = wordLesson.Id
                };
                await _userWordLessonRepository.Add(userWordLessonItem);
                user.Tickets--;
                await _userRepository.Update(user);
            }

            GetWordLessonDto? dto = _mapper.Map<GetWordLessonDto>(wordLesson);

            return dto;
        }

        public async Task SaveBestResult(int wordLessonId, string? userIdClaim, AddWordResultDto dto)
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

            _serviceValidator.ValidateId(wordLessonId);

            WordLesson? wordLesson = await _wordRepository.Get(wordLessonId);

            if (wordLesson == null)
            {
                throw new NotFoundException("Nie znaleziono lekcji.");
            }

            var storySeason = _wordRepository.GetBySeason(userId, wordLesson.StorySeasonId);

            var result = await storySeason.FirstOrDefaultAsync();

            if (result != null)
            {
                if (result.StorySeason != null)
                {
                    if (user.Stars < result.StorySeason.PointsRequiredToUnlock)
                    {
                        throw new BadRequestException("Nie masz wystarczającej ilości gwiazdek, by odpowiadać w tej lekcji.");
                    }
                }
            }

            UserWordLesson? userWordLesson = user.UserWordLessons.SingleOrDefault(u => u.WordLessonId == wordLessonId);

            int totalWords = wordLesson.Words.Count;
            int correctWords = wordLesson.Words.Count();

            double percentageScore = (double)dto.CorrectAnswersCount / totalWords * 100;

            bool isFirstAttempt;

            if (userWordLesson == null)
            {
                isFirstAttempt = true;
            }
            else
            {
                isFirstAttempt = false;
            }

            if (userWordLesson == null)
            {
                userWordLesson = new UserWordLesson
                {
                    UserId = userId,
                    WordLessonId = wordLessonId,
                    BestResult = dto.Result,
                    PercentageScore = percentageScore         
                };

                await _wordRepository.SaveBestResult(userWordLesson);
            }

            if (isFirstAttempt && dto.Result >= 50)
            {
                user.Tickets++;
            }

            if (dto.Result >= 50 && userWordLesson.PercentageScore < 50)
            {
                user.Tickets++;
            }

            if (userWordLesson!.BestResult < dto.Result)
            {
                userWordLesson.BestResult = dto.Result;
                userWordLesson.PercentageScore = percentageScore;
                await _userWordLessonRepository.Update(userWordLesson);
            }

            user.Stars = user.UserStories.Sum(us => us.BestResult) + user.UserWordLessons.Sum(uwl => uwl.BestResult);

            await _userRepository.UpdateStars(user);
        }
    }
}
