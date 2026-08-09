using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IGetStoryRepository
    {
        IQueryable<Story> GetUserStories(int userId);
        IQueryable<Story> GetBySeason(int userId, int? seasonId);
        Task<List<StorySeason>> GetStorySeasonsAsync();
    }
}
