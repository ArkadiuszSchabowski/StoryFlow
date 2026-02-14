using FluentAssertions;
using StoryFlow.Helpers;

namespace StoryFlow_UnitTests.Helpers
{
    public class TextConverterUnitTests
    {
        private readonly TextConverter _textConverter;
        private const string _correctText = "I have a little dog. He’s very cute, but sometimes a bit naughty.";
        public TextConverterUnitTests()
        {
            _textConverter = new TextConverter();
        }
        [Fact]
        public void GetSentencesFromText_WhenCorrectText_ReturnsListSentence()
        {
            var result = _textConverter.GetSentencesFromText(_correctText);

            result.Should().BeOfType<List<string>>();
        }

        [Fact]
        public void GetSentencesFromText_WhenTextIsValid_ReturnsSentencesInCorrectOrder()
        {
            var expectedSentences = new List<string>
            {
                "I have a little dog.",
                "He’s very cute, but sometimes a bit naughty."
            };
            var result = _textConverter.GetSentencesFromText(_correctText);

            result.Should().Equal(expectedSentences);
        }
    }
}
