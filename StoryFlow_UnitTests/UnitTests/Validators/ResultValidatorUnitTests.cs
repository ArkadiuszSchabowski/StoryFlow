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
        [InlineData(StorySize.Short, LanguageLevel.A1, 101, 100)]
        [InlineData(StorySize.Medium, LanguageLevel.A1, 111, 110)]
        [InlineData(StorySize.Long, LanguageLevel.A1, 121, 120)]

        [InlineData(StorySize.Short, LanguageLevel.A2, 121, 120)]
        [InlineData(StorySize.Medium, LanguageLevel.A2, 131, 130)]
        [InlineData(StorySize.Long, LanguageLevel.A2, 141, 140)]

        [InlineData(StorySize.Short, LanguageLevel.B1, 141, 140)]
        [InlineData(StorySize.Medium, LanguageLevel.B1, 151, 150)]
        [InlineData(StorySize.Long, LanguageLevel.B1, 161, 160)]

        [InlineData(StorySize.Short, LanguageLevel.B2, 161, 160)]
        [InlineData(StorySize.Medium, LanguageLevel.B2, 171, 170)]
        [InlineData(StorySize.Long, LanguageLevel.B2, 181, 180)]

        [InlineData(StorySize.Short, LanguageLevel.C1, 181, 180)]
        [InlineData(StorySize.Medium, LanguageLevel.C1, 191, 190)]
        [InlineData(StorySize.Long, LanguageLevel.C1, 201, 200)]

        [InlineData(StorySize.Short, LanguageLevel.C2, 201, 200)]
        [InlineData(StorySize.Medium, LanguageLevel.C2, 211, 210)]
        [InlineData(StorySize.Long, LanguageLevel.C2, 221, 220)]
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
        [InlineData(StorySize.Short, LanguageLevel.A1, 100, 100)]
        [InlineData(StorySize.Medium, LanguageLevel.A1, 110, 110)]
        [InlineData(StorySize.Long, LanguageLevel.A1, 120, 120)]

        [InlineData(StorySize.Short, LanguageLevel.A2, 120, 120)]
        [InlineData(StorySize.Medium, LanguageLevel.A2, 130, 130)]
        [InlineData(StorySize.Long, LanguageLevel.A2, 140, 140)]

        [InlineData(StorySize.Short, LanguageLevel.B1, 140, 140)]
        [InlineData(StorySize.Medium, LanguageLevel.B1, 150, 150)]
        [InlineData(StorySize.Long, LanguageLevel.B1, 160, 160)]

        [InlineData(StorySize.Short, LanguageLevel.B2, 160, 160)]
        [InlineData(StorySize.Medium, LanguageLevel.B2, 170, 170)]
        [InlineData(StorySize.Long, LanguageLevel.B2, 180, 180)]

        [InlineData(StorySize.Short, LanguageLevel.C1, 180, 180)]
        [InlineData(StorySize.Medium, LanguageLevel.C1, 190, 190)]
        [InlineData(StorySize.Long, LanguageLevel.C1, 200, 200)]

        [InlineData(StorySize.Short, LanguageLevel.C2, 200, 200)]
        [InlineData(StorySize.Medium, LanguageLevel.C2, 210, 210)]
        [InlineData(StorySize.Long, LanguageLevel.C2, 220, 220)]
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
