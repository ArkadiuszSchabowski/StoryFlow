using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateUserRepository : IRepository<User>, IGetAllRepository<User>, IGetByEmailRepository
    {

    }
}
