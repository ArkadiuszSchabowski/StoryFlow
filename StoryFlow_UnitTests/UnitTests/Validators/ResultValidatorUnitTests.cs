using FluentAssertions;
using Moq;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Validators;
using StoryFlow_Shared.Enums;

namespace StoryFlow_Tests.UnitTests.Validators
{
    public class ResultValidatorUnitTests
    {
        private readonly Mock<IPointsCalculator> _pointsCalculatorMock;
        private readonly ResultValidator _validator;

        public ResultValidatorUnitTests()
        {
            _pointsCalculatorMock = new Mock<IPointsCalculator>();
            _validator = new ResultValidator(_pointsCalculatorMock.Object);
        }

        [Theory]
        [InlineData(StorySize.Short, LanguageLevel.A1, 51, 50)]
        [InlineData(StorySize.Medium, LanguageLevel.A1, 61, 60)]
        [InlineData(StorySize.Long, LanguageLevel.A1, 71, 70)]

        [InlineData(StorySize.Short, LanguageLevel.A2, 71, 70)]
        [InlineData(StorySize.Medium, LanguageLevel.A2, 81, 80)]
        [InlineData(StorySize.Long, LanguageLevel.A2, 91, 90)]

        [InlineData(StorySize.Short, LanguageLevel.B1, 91, 90)]
        [InlineData(StorySize.Medium, LanguageLevel.B1, 101, 100)]
        [InlineData(StorySize.Long, LanguageLevel.B1, 111, 110)]

        [InlineData(StorySize.Short, LanguageLevel.B2, 111, 110)]
        [InlineData(StorySize.Medium, LanguageLevel.B2, 121, 120)]
        [InlineData(StorySize.Long, LanguageLevel.B2, 131, 130)]

        [InlineData(StorySize.Short, LanguageLevel.C1, 131, 130)]
        [InlineData(StorySize.Medium, LanguageLevel.C1, 141, 140)]
        [InlineData(StorySize.Long, LanguageLevel.C1, 151, 150)]

        [InlineData(StorySize.Short, LanguageLevel.C2, 151, 150)]
        [InlineData(StorySize.Medium, LanguageLevel.C2, 161, 160)]
        [InlineData(StorySize.Long, LanguageLevel.C2, 171, 170)]
        public void ValidateResult_WhenResultIsBiggerThanMaxPoints_ShouldThrowBadRequestException(
            StorySize size,
            LanguageLevel level,
            int result,
            int maxPoints)
        {
            _pointsCalculatorMock
                .Setup(x => x.SetMaxPoints(size, level))
                .Returns(maxPoints);

            var action = () => _validator.ValidateResult(size, level, result);

            action.Should().Throw<BadRequestException>().WithMessage($"Result must be between 0 and {maxPoints}.");
        }

        [Theory]
        [InlineData(StorySize.Short, LanguageLevel.A1, 50, 50)]
        [InlineData(StorySize.Medium, LanguageLevel.A1, 60, 60)]
        [InlineData(StorySize.Long, LanguageLevel.A1, 70, 70)]

        [InlineData(StorySize.Short, LanguageLevel.A2, 70, 70)]
        [InlineData(StorySize.Medium, LanguageLevel.A2, 80, 80)]
        [InlineData(StorySize.Long, LanguageLevel.A2, 90, 90)]

        [InlineData(StorySize.Short, LanguageLevel.B1, 90, 90)]
        [InlineData(StorySize.Medium, LanguageLevel.B1, 100, 100)]
        [InlineData(StorySize.Long, LanguageLevel.B1, 110, 110)]

        [InlineData(StorySize.Short, LanguageLevel.B2, 110, 110)]
        [InlineData(StorySize.Medium, LanguageLevel.B2, 120, 120)]
        [InlineData(StorySize.Long, LanguageLevel.B2, 130, 130)]

        [InlineData(StorySize.Short, LanguageLevel.C1, 130, 130)]
        [InlineData(StorySize.Medium, LanguageLevel.C1, 140, 140)]
        [InlineData(StorySize.Long, LanguageLevel.C1, 150, 150)]

        [InlineData(StorySize.Short, LanguageLevel.C2, 150, 150)]
        [InlineData(StorySize.Medium, LanguageLevel.C2, 160, 160)]
        [InlineData(StorySize.Long, LanguageLevel.C2, 170, 170)]
        public void ValidateResult_WhenResultEqualsMaxPoints_ShouldNotThrowException(
            StorySize size,
            LanguageLevel level,
            int result,
            int maxPoints)
        {
            _pointsCalculatorMock
                .Setup(x => x.SetMaxPoints(size, level))
                .Returns(maxPoints);

            var action = () => _validator.ValidateResult(size, level, result);

            action.Should().NotThrow();
        }
    }
}
