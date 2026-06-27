using AutoMapper;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Interfaces.Repositories;
using StoryFlow.Interfaces.Validators;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class QuizService : IQuizService
    {
        private readonly IAggregateStoryRepository _storyRepository;
        private readonly IEntityValidator<Story> _validator;
        private readonly IAnswerChecker _answerChecker;
        private readonly IMapper _mapper;
        private readonly IUserStoryRepository _userStoryRepositoryy;
        private readonly IAggregateUserRepository _userRepository;

        public QuizService(IAggregateStoryRepository storyRepository, IAggregateUserRepository userRepository, IEntityValidator<Story> validator, IAnswerChecker answerChecker, IMapper mapper, IUserStoryRepository userStoryRepositoryy)
        {
            _storyRepository = storyRepository;
            _validator = validator;
            _answerChecker = answerChecker;
            _mapper = mapper;
            _userStoryRepositoryy = userStoryRepositoryy;
            _userRepository = userRepository;
        }
        public async Task Add(AddQuizDto dto)
        {
            Story? result = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(result);

            if (result!.Quiz != null)
            {
                throw new BadRequestException($"Story '{result.EnglishTitle}' (Id: {result.Id}) already has a quiz.");
            }

            Quiz quiz = _mapper.Map<Quiz>(dto);

            result!.Quiz = quiz;

            await _storyRepository.SaveChangesAsync();
        }

        public async Task<int> CheckAnswers(QuizSubmissionDto dto, int userId)
        {
            Story? result = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(result);

            if (result!.Quiz == null)
            {
                throw new NotFoundException("This story don't have active quiz");
            }

            int quizPoints = _answerChecker.CheckAnswers(result, dto.Answers);

            var user = await _userRepository.Get(userId);

            UserStory? userStory = user!.UserStories.SingleOrDefault(u => u.StoryId == dto.StoryId);

            if (userStory == null)
            {
                var newUserStory = new UserStory
                {
                    UserId = userId,
                    StoryId = dto.StoryId,
                    BestResult = quizPoints
                };

                await _userStoryRepositoryy.Add(newUserStory);

            }
            else if (userStory.BestResult < quizPoints)
            {
                userStory.BestResult = quizPoints;
                await _userStoryRepositoryy.Update(userStory);
            }


            user.Stars = user.UserStories.Sum(us => us.BestResult);

            await _userRepository.UpdateStars(user);

            return quizPoints;
        }
    }
}
