using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IStoryRepository
    {
        IQueryable<Story> Get();
    }
}
