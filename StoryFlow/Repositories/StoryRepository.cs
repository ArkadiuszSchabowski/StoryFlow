using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class StoryRepository : IAggregateStoryRepository
    {
        private readonly MyDbContext _context;

        public StoryRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task Add(Story item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Story?> Get(int id)
        {
            return await _context.Stories.Include(s => s.Sentences).FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<Story> Get()
        {
            return _context.Stories.Include(s => s.UserStories).Include(s => s.Sentences).AsQueryable();
        }

        public async Task AddBestResult(UserStory userStory)
        {
            await _context.UserStories.AddAsync(userStory);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Story story)
        {
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();
        }

        public async Task<UserStory?> GetByUserAndStory(int userId, int storyId)
        {
            return await _context.UserStories
                .FirstOrDefaultAsync(x => x.UserId == userId && x.StoryId == storyId);
        }

        public async Task Update(UserStory userStory)
        {
            _context.UserStories.Update(userStory);
            await _context.SaveChangesAsync();
        }
    }
}
