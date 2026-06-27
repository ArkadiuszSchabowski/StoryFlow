using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly MyDbContext _context;

        public UserStoryRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task Add(UserStory item)
        {
            await _context.UserStories.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task AddBestResult(UserStory userStory)
        {
            await _context.UserStories.AddAsync(userStory);
            await _context.SaveChangesAsync();
        }

        public async Task<UserStory?> GetByUserAndStory(int userId, int storyId)
        {
            return await _context.UserStories
                .FirstOrDefaultAsync(x => x.UserId == userId && x.StoryId == storyId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserStory userStory)
        {
            _context.UserStories.Update(userStory);
            await _context.SaveChangesAsync();
        }
    }
}
