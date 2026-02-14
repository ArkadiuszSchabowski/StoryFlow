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

        private readonly string _tooShortStory = new string('A', 149);
        private readonly string _validShortStory = new string('A', 150);
        private readonly string _validLongStory = new string('A', 1000);
        private readonly string _tooLongStory = new string('A', 1001);

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
                EnglishStory = _validShortStory
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
                EnglishStory = _validShortStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Title is required.");
        }

        [Fact]
        public void Validate_WhenStoryIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = null
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Story is required.");
        }

        [Fact]
        public void Validate_WhenStoryIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                Title = _validTitle,
                EnglishStory = "   "
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Story is required.");
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
                EnglishStory = _validShortStory
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
                EnglishStory = _validShortStory
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
                EnglishStory = _validShortStory
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
                EnglishStory = _tooShortStory
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
                EnglishStory = _tooLongStory
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
                EnglishStory = _validShortStory
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
                EnglishStory = _validLongStory
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
