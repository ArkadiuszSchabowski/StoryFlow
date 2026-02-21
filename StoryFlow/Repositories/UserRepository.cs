using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public Task Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
