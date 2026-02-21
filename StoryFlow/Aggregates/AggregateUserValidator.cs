using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;

namespace StoryFlow.Aggregates
{
    public class AggregateUserValidator : IAggregateUserValidator
    {
        private readonly IValidatorId _validatorId;
        private readonly IEntityValidator<User> _entityValidator;

        public AggregateUserValidator(IValidatorId validatorId, IEntityValidator<User> entityValidator)
        {
            _validatorId = validatorId;
            _entityValidator = entityValidator;
        }
        public void ThrowIsNull(User? entity)
        {
            _entityValidator.ThrowIsNull(entity);
        }

        public void ValidateId(int? id)
        {
            _validatorId.ValidateId(id);
        }
    }
}
