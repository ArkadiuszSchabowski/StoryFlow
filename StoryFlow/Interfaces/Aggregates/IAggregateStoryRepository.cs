using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateStoryRepository : IRepository<Story>, IGetStoryRepository
    {
        Task SaveChangesAsync();
    }
}
