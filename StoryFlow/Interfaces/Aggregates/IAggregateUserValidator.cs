using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateUserValidator : IUserValidator, IValidatorId, IEntityValidator<User>
    {
    }
}
