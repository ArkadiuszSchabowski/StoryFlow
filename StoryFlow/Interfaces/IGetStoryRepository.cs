using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IGetStoryRepository
    {
        IQueryable<Story> Get();
    }
}
