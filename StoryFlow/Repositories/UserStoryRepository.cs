using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class UserStoryRepository : IUserStoryRepositoryy
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
