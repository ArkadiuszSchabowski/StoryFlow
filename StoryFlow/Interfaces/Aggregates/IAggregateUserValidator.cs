using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateUserValidator : IValidatorId, IEntityValidator<User>
    {
    }
}
