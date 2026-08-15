using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface ISeasonService
    {
        Task<GetStorySeasonDto> GetBySeason(int seasonId, string? userIdClaim);
    }
}
