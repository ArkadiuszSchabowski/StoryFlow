using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IGetStorySeason
    {
        Task<List<GetStoryDto>> GetBySeason(int seasonId, string? userIdClaim);
        Task<List<GetStorySeasonDto>> GetSeasons(string? userIdClaim);
    }
}
