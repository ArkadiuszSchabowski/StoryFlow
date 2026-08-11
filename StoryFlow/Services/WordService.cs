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

        public WordService(IAggregateUserRepository userRepository, IAggregateWordRepository wordRepository, IAggregateStoryValidator serviceValidator, IMapper mapper)
        {
            _userRepository = userRepository;
            _wordRepository = wordRepository;
            _serviceValidator = serviceValidator;
            _mapper = mapper;
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

            GetWordLessonDto? dto = _mapper.Map<GetWordLessonDto>(wordLesson);

            return dto;
        }
    }
}
