using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Repositories
{
    public class UserRepository : IRepository<User>, IGetAllRepository<User>, IGetByEmailRepository
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

        public async Task<User?> Get(int id)
        {
            return await _context.Users.Include(u => u.UserHobbies).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task Remove(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
