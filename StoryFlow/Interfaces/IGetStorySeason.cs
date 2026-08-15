using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IGetStorySeason
    {
        Task<List<GetStorySeasonDto>> GetSeasons(string? userIdClaim);
    }
}
