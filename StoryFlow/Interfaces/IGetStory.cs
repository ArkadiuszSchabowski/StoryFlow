using StoryFlow_Shared.Models;

namespace StoryFlow_Shared.Interfaces
{
    public interface IGetStory
    {
        Task<ICollection<GetStoryDto>> Get(StoryFilter? storyFilter, int userId);
        Task<GetStoryDto> Get(int id, string? userIdClaim);
        Task<GetStoryDto> GetWelcomeStory();
    }
}
