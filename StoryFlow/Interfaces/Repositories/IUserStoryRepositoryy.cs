using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IUserStoryRepositoryy : IAdd<UserStory>
    {
        Task SaveChangesAsync();
        Task Update(UserStory userStory);
    }
}
