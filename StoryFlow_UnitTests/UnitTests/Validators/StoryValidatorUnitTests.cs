using FluentAssertions;
using StoryFlow.Exceptions;
using StoryFlow.Validators;
using StoryFlow_Shared.Models;

namespace StoryFlow_Tests.UnitTests.Validators
{
    
    public class StoryValidatorUnitTests
    {
        private readonly StoryValidator _storyValidator;

        private const string _tooShortTitle = "Dogs";
        private readonly string _validTitle = new string('A', 50);
        private readonly string _tooLongTitle = new string('A', 51);

        private readonly string _tooShortEnglishStory = new string('A', 149);
        private readonly string _validShortEnglishStory = new string('A', 150);
        private readonly string _validLongEnglishStory = new string('A', 1000);
        private readonly string _tooLongEnglishStory = new string('A', 1001);

        private readonly string _validTranslatedPolishStory = "valid translated story";

        public StoryValidatorUnitTests()
        {
            _storyValidator = new StoryValidator();
        }
        [Fact]
        public void Validate_WhenTitleIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = null,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Title is required.");
        }

        [Fact]
        public void Validate_WhenTitleIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = "   ",
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Title is required.");
        }

        [Fact]
        public void Validate_WhenEnglishStoryIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = null,
                PolishStory = "valid polish story"
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("English story is required.");
        }

        [Fact]
        public void Validate_WhenPolishStoryIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = null
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polish story is required.");
        }

        [Fact]
        public void Validate_WhenEnglishStoryIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = "   ",
                PolishStory = "valid polish story"
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("English story is required.");
        }

        [Fact]
        public void Validate_WhenPolishStoryIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = "   "
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polish story is required.");
        }

        [Fact]
        public void Validate_WhenDtoIsNull_ThrowsBadRequestException()
        {
            AddStoryDto? dto = null;

            var act = () => _storyValidator.Validate(dto);

            act.Should().Throw<BadRequestException>().WithMessage("Dto is required.");
        }

        [Fact]
        public void Validate_WhenTitleTooShort_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _tooShortTitle,
                EnglishStory = _validShortEnglishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Title must be between 5 and 50 characters long.");
        }

        [Fact]
        public void Validate_WhenTitleTooLong_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _tooLongTitle,
                EnglishStory = _validShortEnglishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Title must be between 5 and 50 characters long.");
        }

        [Fact]
        public void Validate_WhenValidTitle_DontThrowsException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_WhenStoryTooShort_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _tooShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Story must be between 150 and 1000 characters long.");
        }

        [Fact]
        public void Validate_WhenStoryTooLong_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _tooLongEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Story must be between 150 and 1000 characters long.");
        }

        [Fact]
        public void Validate_WhenValidShortStory_DontThrowsException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_WhenValidLongStory_DontThrowsException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = _validLongEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void ValidateSentencesCount_WhenEqualAreTheSame_NotThrowsException(int firstSentenceCount, int secondSentenceCount)
        {
            var act = () => _storyValidator.ValidateSentencesCount(firstSentenceCount, secondSentenceCount);

            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(0,1)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(9, 10)]

        public void ValidateSentencesCount_WhenEqualIsDifferent_ThrowsException(int firstSentenceCount, int secondSentenceCount)
        {
            var act = () => _storyValidator.ValidateSentencesCount(firstSentenceCount, secondSentenceCount);

            act.Should().Throw<BadRequestException>().WithMessage("Polish sentences are not equal to english sentences.");
        }
    }
}
