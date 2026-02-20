using StoryFlow_Shared.Models;

namespace StoryFlow_Shared.Interfaces
{
    public interface IGetStory
    {
        Task<ICollection<GetStoryDto>> Get(StoryFilter storyFilter);
        Task<GetStoryDto> Get(int id);
    }
}
