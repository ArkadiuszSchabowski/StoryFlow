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
            await _context.Stories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Story?> Get(int id)
        {
            return await _context.Stories.Include(s => s.Sentences).Include(s => s.StoryPoint).Include(s => s.Quiz!).ThenInclude(q => q.Questions)
                .ThenInclude(q => q.Answers).FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<Story> GetUserStories(int userId)
        {
            return _context.Stories.Include(s => s.UserStories.Where(us => us.UserId == userId)).Include(s => s.Sentences).Include(s => s.Quiz!).Include(s => s.StoryPoint).AsQueryable();
        }

        public  IQueryable<Story> GetBySeason(int userId, int? seasonId)
        {
            return _context.Stories.Include(s => s.UserStories.Where(us => us.UserId == userId)).Include(s => s.Sentences).Include(s => s.Quiz!).Include(s => s.StoryPoint).Include(s => s.StorySeason).Where(s => s.StorySeasonId == seasonId).OrderBy(s => s.OrderInSeason).AsQueryable();
        }

        public async Task<List<StorySeason>> GetStorySeasonsAsync()
        {
            return await _context.StorySeazons
                .Include(s => s.Stories).ThenInclude(ss => ss.StoryPoint)
                .Include(s => s.Stories).ThenInclude(s => s.UserStories)
                .Include(s => s.WordLessons).ThenInclude(wl => wl.UserWordLessons)
                .ToListAsync();
        }

        public async Task Remove(Story story)
        {
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(Story entity)
        {
            throw new NotImplementedException();
        }
    }
}
