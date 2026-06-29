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
        private readonly IUserStoryRepository _userStoryRepository;
        private readonly IPointsCalculator _pointsCalculator;
        private readonly IAggregateUserRepository _userRepository;

        public QuizService(IAggregateStoryRepository storyRepository, IAggregateUserRepository userRepository, IEntityValidator<Story> validator, IAnswerChecker answerChecker, IMapper mapper, IUserStoryRepository userStoryRepository, IPointsCalculator pointsCalculator)
        {
            _storyRepository = storyRepository;
            _validator = validator;
            _answerChecker = answerChecker;
            _mapper = mapper;
            _userStoryRepository = userStoryRepository;
            _pointsCalculator = pointsCalculator;
            _userRepository = userRepository;
        }
        public async Task Add(AddQuizDto dto)
        {
            Story? story = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(story);

            if (story!.Quiz != null)
            {
                throw new BadRequestException($"Story '{story.EnglishTitle}' (Id: {story.Id}) already has a quiz.");
            }

            int numberOfQuestions = dto.Questions.Count();

            Quiz quiz = _mapper.Map<Quiz>(dto);

            story!.Quiz = quiz;

            if (story.StoryPoint == null)
            {
                story.StoryPoint = new StoryPoint();
            }

            story.StoryPoint.MaxPoints = _pointsCalculator.SetMaxPoints(story.StorySize, story.LanguageLevel, numberOfQuestions);

            story.StoryPoint.PointsPerAnswer = _pointsCalculator.GetPointsPerAnswer(story.LanguageLevel);

            story.StoryPoint.BonusPointsForStoryLength = _pointsCalculator.GetBonusPointsForStoryLength(story.StorySize);

            story.NumberOfQuestions = numberOfQuestions;

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

                await _userStoryRepository.Add(newUserStory);

            }
            else if (userStory.BestResult < quizPoints)
            {
                userStory.BestResult = quizPoints;
                await _userStoryRepository.Update(userStory);
            }


            user.Stars = user.UserStories.Sum(us => us.BestResult);

            await _userRepository.UpdateStars(user);

            return quizPoints;
        }
    }
}
