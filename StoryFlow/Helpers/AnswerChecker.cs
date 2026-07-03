using StoryFlow.Interfaces;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Helpers
{
    public class AnswerChecker : IAnswerChecker
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IPointsCalculator _pointsCalculator;

        public AnswerChecker(IQuizRepository quizRepository, IPointsCalculator pointsCalculator)
        {
            _quizRepository = quizRepository;
            _pointsCalculator = pointsCalculator;
        }
        public QuizResult CheckAnswers(Story story, List<AnswerDto> dto)
        {
            int userQuizPoints = 0;
            int bonusQuizPoints = _pointsCalculator.GetBonusPointsForStoryLength(story.StorySize);
            int pointsPerAnswer = _pointsCalculator.GetPointsPerAnswer(story.LanguageLevel);

            userQuizPoints += bonusQuizPoints;

            foreach (Question question in story.Quiz!.Questions)
            {
                var userAnswer = dto.FirstOrDefault(a => a.QuestionId == question.Id);

                if (userAnswer == null)
                    continue;

                int correctAnswerId = _quizRepository.GetCorrectAnswer(question);

                if (userAnswer.AnswerId == correctAnswerId)
                {
                    userQuizPoints += pointsPerAnswer;
                }
            }

            double scorePercentage = (userQuizPoints / (double)story.StoryPoint!.MaxPoints!) * 100;

            var quizResult = new QuizResult
            {
                Points = userQuizPoints,
                ScorePercentage = scorePercentage
            };

            return quizResult;
        }

    }
}

