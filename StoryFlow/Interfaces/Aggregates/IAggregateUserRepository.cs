using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateUserRepository : IRepository<User>, IGetAllRepository<User>, IGetByEmailRepository, IUpdateStars
    {

    }
}
