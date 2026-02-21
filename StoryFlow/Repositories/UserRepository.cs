using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Repositories
{
    public class UserRepository : IRepository<User>, IGetAllRepository<User>
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task Add(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task<User?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<User>> Get()
        {
            return await _context.Users.ToListAsync();
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
