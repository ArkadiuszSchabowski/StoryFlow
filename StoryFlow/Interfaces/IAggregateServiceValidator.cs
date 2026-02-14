using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IAggregateServiceValidator : IValidator<AddStoryDto>, IValidatorId, IEntityValidator<Story>
    {

    }
}
