using FluentAssertions;
using StoryFlow.Helpers;
using StoryFlow_Shared.Enums;

namespace StoryFlow_Tests.UnitTests.Helpers
{
    public class TextCounterUnitTests
    {
        private readonly TextCounter _textCounter;
        public TextCounterUnitTests()
        {
            _textCounter = new TextCounter();
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(399, null)]
        [InlineData(400, StorySize.Short)]
        [InlineData(699, StorySize.Short)]
        [InlineData(700, StorySize.Medium)]
        [InlineData(999, StorySize.Medium)]
        [InlineData(1000, StorySize.Long)]
        [InlineData(1500, StorySize.Long)]
        [InlineData(1501, null)]
        public void SetTextSize_WhenCalled_ReturnsCorrectTextSize(int length, StorySize? expectedResult)
        {
            var text = new string('A', length);

            var result = _textCounter.SetTextSize(text);

            result.Should().Be(expectedResult);
        }
    }
}
