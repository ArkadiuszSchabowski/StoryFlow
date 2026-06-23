using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Enums;

namespace StoryFlow.Validators
{
    public class ResultValidator : IResultValidator
    {
        private readonly IPointsCalculator _pointsCalculator;

        public ResultValidator(IPointsCalculator pointsCalculator)
        {
            _pointsCalculator = pointsCalculator;
        }

        public void ValidateResult(
            StorySize? size,
            LanguageLevel? languageLevel,
            int result)
        {
            var maxPoints = _pointsCalculator.SetMaxPoints(size, languageLevel);

            if (result < 0 || result > maxPoints)
            {
                throw new BadRequestException(
                    $"Result must be between 0 and {maxPoints}.");
            }
        }
    }
}