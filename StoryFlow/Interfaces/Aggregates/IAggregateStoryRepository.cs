using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateStoryRepository : IRepository<Story>, IGetStoryRepository, IUserStoryRepository
    {

    }
}
