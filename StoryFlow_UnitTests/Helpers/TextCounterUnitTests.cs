using FluentAssertions;
using StoryFlow.Helpers;
using StoryFlow_Shared.Enums;

namespace StoryFlow_UnitTests.Helpers
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
        [InlineData(149, null)]
        [InlineData(150, StorySize.Short)]
        [InlineData(299, StorySize.Short)]
        [InlineData(300, StorySize.Medium)]
        [InlineData(599, StorySize.Medium)]
        [InlineData(600, StorySize.Long)]
        [InlineData(1000, StorySize.Long)]
        [InlineData(1001, null)]
        public void SetTextSize_WhenCalled_ReturnsCorrectTextSize(int length, StorySize? expectedResult)
        {
            var text = new string('A', length);

            var result = _textCounter.SetTextSize(text);

            result.Should().Be(expectedResult);
        }
    }
}
