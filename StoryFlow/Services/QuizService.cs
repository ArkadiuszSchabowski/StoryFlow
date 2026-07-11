using AutoMapper;
using Microsoft.Extensions.Options;
using StoryFlow.Exceptions;
using StoryFlow.Helpers;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Interfaces.Repositories;
using StoryFlow.Interfaces.Validators;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;
using System.Text;
using System.Text.Json;

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
        private readonly GeminiSchemaGenerator _geminiSchemaGenerator;
        private readonly IAggregateUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly GeminiSettings _geminiSettings;

        public QuizService(IAggregateStoryRepository storyRepository, IAggregateUserRepository userRepository, IQuestionRepository questionRepository, IEntityValidator<Story> validator, IAnswerChecker answerChecker, IMapper mapper, IUserStoryRepository userStoryRepository, IAnswerRepository answerRepository, IPointsCalculator pointsCalculator, GeminiSchemaGenerator geminiSchemaGenerator, IOptions<GeminiSettings> geminiSettings)
        {
            _storyRepository = storyRepository;
            _validator = validator;
            _answerChecker = answerChecker;
            _mapper = mapper;
            _userStoryRepository = userStoryRepository;
            _pointsCalculator = pointsCalculator;
            _geminiSchemaGenerator = geminiSchemaGenerator;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _geminiSettings = geminiSettings.Value;
        }
        public async Task Add(AddQuizDto dto)
        {
            Story? story = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(story);

            if (story!.Quiz != null)
            {
                throw new BadRequestException($"Historia '{story.PolishTitle}' (Id: {story.Id}) posiada już quiz.");
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

        public async Task<QuestionSubmissionResult> CheckAnswer(QuestionSubmissionDto dto, string userIdString)
        {
            if (userIdString == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            var userId = int.Parse(userIdString);

            Story? story = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(story);

            if (story!.Quiz == null)
            {
                throw new NotFoundException("Historia nie ma dostępnego quizu.");
            }

            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            Question? question = await _questionRepository.Get(dto.QuestionId);

            if (question == null)
            {
                throw new NotFoundException("Nie znaleziono pytania.");
            }

            Answer? answer = await _answerRepository.Get(dto.AnswerId);

            if (answer == null)
            {
                throw new NotFoundException("Nie znaleziono odpowiedzi.");
            }

            return new QuestionSubmissionResult
            {
                IsCorrect = answer.IsCorrect,
                CorrectAnswerId = question.Answers
                .First(x => x.IsCorrect)
                .Id
            };
        }

        public async Task<QuizResult> CheckAnswers(QuizSubmissionDto dto, string? userIdString)
        {
            if (userIdString == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            var userId = int.Parse(userIdString);

            Story? story = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(story);

            if (story!.Quiz == null)
            {
                throw new NotFoundException("Historia nie ma dostępnego quizu.");
            }

            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            QuizResult quizResult = new QuizResult();
            quizResult = _answerChecker.CheckAnswers(story, dto.Answers);

            UserStory? userStory = user.UserStories.SingleOrDefault(u => u.StoryId == dto.StoryId);

            if (userStory == null)
            {
                var newUserStory = new UserStory
                {
                    UserId = userId,
                    StoryId = dto.StoryId,
                    BestResult = quizResult.Points,
                    PercentageScore = quizResult.ScorePercentage
                };

                await _userStoryRepository.Add(newUserStory);

            }

            if (quizResult.ScorePercentage >= 50 && userStory!.PercentageScore < 50)
            {
                user.Tickets++;
            }

            if (userStory!.BestResult < quizResult.Points)
            {
                userStory.BestResult = quizResult.Points;
                userStory.PercentageScore = quizResult.ScorePercentage;
                await _userStoryRepository.Update(userStory);
            }

            user.Stars = user.UserStories.Sum(us => us.BestResult);

            await _userRepository.UpdateStars(user);

            return quizResult;
        }

        public async Task<object?> Generate(GenerateQuizDto dto)
        {
            object schema = _geminiSchemaGenerator.GenerateStoryQuizSchema();

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
                    Write a quiz in English based on {dto.EnglishStory}.

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
    }
}
