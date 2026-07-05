using FluentAssertions;
using StoryFlow.Exceptions;
using StoryFlow.Validators;
using StoryFlow_Database.Entities;

namespace StoryFlow_Tests.UnitTests.Validators
{
    public class StoryEntityValidatorUnitTests
    {
        private readonly StoryEntityValidator _storyEntityValidator;
        public StoryEntityValidatorUnitTests()
        {
            _storyEntityValidator = new StoryEntityValidator();
        }

        [Fact]
        public void ThrowIsNull_WhenEntityIsNull_ThrowsNotFoundException()
        {
            Story? story = null;

            var act = () => _storyEntityValidator.ThrowIsNull(story);

            act.Should().Throw<NotFoundException>().WithMessage("Nie znaleziono historii.");
        }

        [Fact]
        public void ThrowIsNull_WhenEntityIsNotNull_DoesNotThrowException()
        {
            Story? story = new Story();

            var act = () => _storyEntityValidator.ThrowIsNull(story);

            act.Should().NotThrow();
        }
    }
}
