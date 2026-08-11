using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class WordRepository : IAggregateWordRepository
    {
        private readonly MyDbContext _context;

        public WordRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<WordLesson?> Get(int id)
        {
            return await _context.WordLessons.Include(w => w.WordPoint).Include(w => w.Words).FirstOrDefaultAsync(w => w.Id == id);
        }
        public IQueryable<Story> GetBySeason(int userId, int? seasonId)
        {
            return _context.Stories.Include(s => s.UserStories.Where(us => us.UserId == userId)).Include(s => s.Sentences).Include(s => s.Quiz!).Include(s => s.StoryPoint).Include(s => s.StorySeason).Where(s => s.StorySeasonId == seasonId).OrderBy(s => s.OrderInSeason).AsQueryable();
        }
    }
}
