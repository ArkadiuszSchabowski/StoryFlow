using FluentAssertions;
using StoryFlow.Exceptions;
using StoryFlow.Helpers;
using StoryFlow_Shared.Enums;

namespace StoryFlow_Tests.UnitTests.Helpers
{
    public class PointsCalculatorUnitTests
    {
        private readonly PointsCalculator _pointsCalculator;

        public PointsCalculatorUnitTests()
        {
            _pointsCalculator = new PointsCalculator();
        }

        [Theory]
        [InlineData(StorySize.Short, LanguageLevel.A1, 4, 50)]
        [InlineData(StorySize.Medium, LanguageLevel.A1, 4, 60)]
        [InlineData(StorySize.Long, LanguageLevel.A1, 4, 70)]

        [InlineData(StorySize.Short, LanguageLevel.A2, 4, 70)]
        [InlineData(StorySize.Medium, LanguageLevel.A2, 4, 80)]
        [InlineData(StorySize.Long, LanguageLevel.A2, 4, 90)]

        [InlineData(StorySize.Short, LanguageLevel.B1, 4, 90)]
        [InlineData(StorySize.Medium, LanguageLevel.B1, 4, 100)]
        [InlineData(StorySize.Long, LanguageLevel.B1, 4, 110)]

        [InlineData(StorySize.Short, LanguageLevel.B2, 4, 110)]
        [InlineData(StorySize.Medium, LanguageLevel.B2, 4, 120)]
        [InlineData(StorySize.Long, LanguageLevel.B2, 4, 130)]

        [InlineData(StorySize.Short, LanguageLevel.C1, 4, 130)]
        [InlineData(StorySize.Medium, LanguageLevel.C1, 4, 140)]
        [InlineData(StorySize.Long, LanguageLevel.C1, 4, 150)]

        [InlineData(StorySize.Short, LanguageLevel.C2, 4, 150)]
        [InlineData(StorySize.Medium, LanguageLevel.C2, 4, 160)]
        [InlineData(StorySize.Long, LanguageLevel.C2, 4, 170)]
        public void SetMaxPoints_WhenCalled_ShouldReturnCorrectValue(
            StorySize size,
            LanguageLevel level,
            int questions,
            int expectedResult)
        {
            var result = _pointsCalculator.SetMaxPoints(size, level, questions);

            result.Should().Be(expectedResult);
        }
    }
}
