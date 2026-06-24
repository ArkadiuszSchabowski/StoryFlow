using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow_Shared.Interfaces
{
    public interface IStoryService : IAdd<AddStoryDto>, IGetStory, IRemove, IGenerateStory
    {
        Task SaveStoryBestResultForUser(int userId, int storyId, int result);
    }
}
