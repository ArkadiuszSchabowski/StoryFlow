using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Repositories;
using StoryFlow_Database.Entities;

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
        public int CheckAnswers(Story story, List<AnswerDto> dto)
        {
            int quizPoints = _pointsCalculator.GetBonusPointsForStoryLength(story.StorySize);
            int pointsPerAnswer = _pointsCalculator.GetPointsPerAnswer(story.LanguageLevel);

            foreach (Question question in story.Quiz!.Questions)
            {
                var userAnswer = dto.FirstOrDefault(a => a.QuestionId == question.Id);

                if (userAnswer == null)
                    continue;

                int correctAnswerId = _quizRepository.GetCorrectAnswer(question);

                if (userAnswer.AnswerId == correctAnswerId)
                {
                    quizPoints += pointsPerAnswer;
                }
            }

            return quizPoints;
        }

    }
}

