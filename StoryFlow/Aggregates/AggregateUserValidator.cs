using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Aggregates
{
    public class AggregateUserValidator : IAggregateUserValidator
    {
        private readonly IUserValidator _userValidator;
        private readonly IValidatorId _validatorId;
        private readonly IEntityValidator<User> _entityValidator;

        public AggregateUserValidator(IUserValidator userValidator, IValidatorId validatorId, IEntityValidator<User> entityValidator)
        {
            _userValidator = userValidator;
            _validatorId = validatorId;
            _entityValidator = entityValidator;
        }
        public void ThrowIsNull(User? entity)
        {
            _entityValidator.ThrowIsNull(entity);
        }

        public void ValidateDto(RegisterUserDto dto)
        {
            _userValidator.ValidateDto(dto);
        }

        public void ValidateId(int? id)
        {
            _validatorId.ValidateId(id);
        }
    }
}
