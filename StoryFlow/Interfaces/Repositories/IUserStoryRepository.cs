using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IUserStoryRepository
    {
        Task AddBestResult(UserStory userStory);
        Task<UserStory?> GetByUserAndStory(int userId, int storyId);
        Task Remove(Story entity);
        Task Update(UserStory userStory);
        Task SaveChangesAsync();
    }
}
