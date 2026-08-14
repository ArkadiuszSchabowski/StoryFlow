using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class UserRepository : IRepository<User>, IGetAllRepository<User>, IGetByEmailRepository, IUpdateStars
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
            return await _context.Users
                .Include(u => u.UserHobbies)
                .Include(u => u.UserStories)
                .Include(u => u.UserWordLessons)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByNick(string nick)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Nick == nick);
        }

        public async Task Remove(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserStory userStory)
        {
            _context.UserStories.Update(userStory);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStars(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
