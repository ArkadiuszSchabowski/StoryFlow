using FluentAssertions;
using StoryFlow.Exceptions;
using StoryFlow.Validators;
using StoryFlow_Shared.Models;

namespace StoryFlow_Tests.UnitTests.Validators
{
    public class StoryValidatorUnitTests
    {
        private readonly StoryValidator _storyValidator;

        private readonly string _validEnglishTitle = new string('A', 50);
        private readonly string _validPolishTitle = "Zwierzęca historia";
        private const string _tooShortEnglishTitle = "Dogs";
        private readonly string _tooLongEnglishTitle = new string('A', 51);

        private readonly string _validPolishDescription = "Poprawny opis";
        private readonly string _validEnglishDescription = "Valid English Description";

        private readonly string _validShortEnglishStory = new string('A', 400);
        private readonly string _validLongEnglishStory = new string('A', 1500);
        private readonly string _tooShortEnglishStory = new string('A', 399);
        private readonly string _tooLongEnglishStory = new string('A', 1501);

        private readonly string _validTranslatedPolishStory = "Valid translated story";

        public StoryValidatorUnitTests()
        {
            _storyValidator = new StoryValidator();
        }

        [Fact]
        public void Validate_WhenPolishTitleIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = null,
                EnglishTitle = _validEnglishTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polski tytuł jest wymagany.");
        }

        [Fact]
        public void Validate_WhenEnglishTitleIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = null,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Angielski tytuł jest wymagany.");
        }

        [Fact]
        public void Validate_WhenPolishTitleIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = "    ",
                EnglishTitle = _validEnglishTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polski tytuł jest wymagany.");
        }

        [Fact]
        public void Validate_WhenEnglishTitleIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = "   ",
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Angielski tytuł jest wymagany.");
        }

        [Fact]
        public void Validate_WhenEnglishStoryIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishStory = null,
                PolishStory = _validTranslatedPolishStory,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Angielska historia jest wymagana.");
        }

        [Fact]
        public void Validate_WhenPolishStoryIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishStory = _validLongEnglishStory,
                PolishStory = null,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polska historia jest wymagana.");
        }

        [Fact]
        public void Validate_WhenEnglishStoryIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishStory = "   ",
                PolishStory = _validTranslatedPolishStory,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Angielska historia jest wymagana.");
        }

        [Fact]
        public void Validate_WhenPolishStoryIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishStory = _validShortEnglishStory,
                PolishStory = "   ",
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polska historia jest wymagana.");
        }

        [Fact]
        public void Validate_WhenDtoIsNull_ThrowsBadRequestException()
        {
            AddStoryDto? dto = null;

            var act = () => _storyValidator.Validate(dto);

            act.Should()
                .Throw<BadRequestException>()
                .WithMessage("Encja jest wymagana.");
        }

        [Fact]
        public void Validate_WhenTooShortEnglishTitle_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _tooShortEnglishTitle,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription,
                PolishStory = _validTranslatedPolishStory,
                EnglishStory = _validShortEnglishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Tytuł w języku angielskim musi mieć od 5 do 50 znaków.");
        }

        [Fact]
        public void Validate_WhenEnglishTitleTooLong_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _tooLongEnglishTitle,
                PolishStory = _validTranslatedPolishStory,
                EnglishStory = _validShortEnglishStory,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription,
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Tytuł w języku angielskim musi mieć od 5 do 50 znaków.");
        }

        [Fact]
        public void Validate_WhenTooShortEnglishStory_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription,
                EnglishStory = _tooShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Historia w języku angielskim musi mieć od 400 do 1500 znaków.");
        }

        [Fact]
        public void Validate_WhenEnglishStoryTooLong_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishStory = _tooLongEnglishStory,
                PolishStory = _validTranslatedPolishStory,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription,
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Historia w języku angielskim musi mieć od 400 do 1500 znaków.");
        }

        [Fact]
        public void Validate_WhenValidStory_DoesNotThrowException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                PolishDescription = _validPolishDescription,
                EnglishDescription = _validEnglishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_WhenEnglishDescriptionIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishDescription = null,
                PolishDescription = _validPolishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Angielski opis jest wymagany.");
        }

        [Fact]
        public void Validate_WhenEnglishDescriptionIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishDescription = "   ",
                PolishDescription = _validPolishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Angielski opis jest wymagany.");
        }

        [Fact]
        public void Validate_WhenTooShortEnglishDescription_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishDescription = new string('A', 9),
                PolishDescription = _validPolishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Opis w języku angielskim musi mieć od 10 do 100 znaków.");
        }

        [Fact]
        public void Validate_WhenTooLongEnglishDescription_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishDescription = new string('A', 101),
                PolishDescription = _validPolishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Opis w języku angielskim musi mieć od 10 do 100 znaków.");
        }

        [Fact]
        public void Validate_WhenValidEnglishDescription_DoesNotThrowException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                EnglishDescription = _validEnglishDescription,
                PolishDescription = _validPolishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_WhenPolishDescriptionIsNull_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                PolishDescription = null,
                EnglishDescription = _validEnglishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polski opis jest wymagany.");
        }

        [Fact]
        public void Validate_WhenPolishDescriptionIsWhiteSpace_ThrowsBadRequestException()
        {
            var dto = new AddStoryDto
            {
                PolishTitle = _validPolishTitle,
                EnglishTitle = _validEnglishTitle,
                PolishDescription = "    ",
                EnglishDescription = _validEnglishDescription,
                EnglishStory = _validShortEnglishStory,
                PolishStory = _validTranslatedPolishStory
            };

            var action = () => _storyValidator.Validate(dto);

            action.Should()
                .Throw<BadRequestException>()
                .WithMessage("Polski opis jest wymagany.");
        }
    }
}