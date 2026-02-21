using StoryFlow_Shared.Models;

namespace StoryFlow_Shared.Interfaces
{
    public interface IStoryService : IAdd<AddStoryDto>, IGetStory, IRemove
    {

    }
}
