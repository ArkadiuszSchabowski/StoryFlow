using StoryFlow.Interfaces.Validators;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateStoryValidator : IValidator<AddStoryDto>, IValidatorId, IEntityValidator<Story>
    {

    }
}
