using FluentAssertions;
using StoryFlow.Exceptions;
using StoryFlow.Validators;

namespace StoryFlow_UnitTests.Validators
{
    public class ValidatorIdUnitTests
    {
        private readonly ValidatorId _validatorId;

        public ValidatorIdUnitTests()
        {
            _validatorId = new ValidatorId();
        }
        [Fact]
        public void ValidateId_WhenIdIsNull_ThrowsBadRequestException()
        {
            int? id = null;

            var act = () => _validatorId.ValidateId(id);

            act.Should().Throw<BadRequestException>().WithMessage("Id is required.");
        }

        [Fact]
        public void ValidateId_WhenIdIsZero_ThrowsBadRequestException()
        {
            int? id = 0;

            var act = () => _validatorId.ValidateId(id);

            act.Should().Throw<BadRequestException>().WithMessage("Id must be greater than 0.");
        }

        [Fact]
        public void ValidateId_WhenIdIsNegative_ThrowsBadRequestException()
        {
            int? id = -1;

            var act = () => _validatorId.ValidateId(id);

            act.Should().Throw<BadRequestException>().WithMessage("Id must be greater than 0.");
        }

        [Fact]
        public void ValidateId_WhenValidId_DoesNotThrowException()
        {
            int? id = 1;

            var act = () => _validatorId.ValidateId(id);

            act.Should().NotThrow();
        }
    }
}
