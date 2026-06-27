using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IGetStoryRepository
    {
        IQueryable<Story> Get();
    }
}
