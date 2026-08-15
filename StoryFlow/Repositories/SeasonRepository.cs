using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly MyDbContext _context;

        public SeasonRepository(MyDbContext context)
        {
            _context = context;
        }
        public StorySeason? GetBySeason(int userId, int? seasonId)
        {
            return _context.StorySeazons
                .Where(x => x.Id == seasonId)
                .Include(x => x.WordLessons).ThenInclude(wl => wl.Words)
                .Include(x => x.WordLessons).ThenInclude(wl => wl.WordPoint)
                .Include(x => x.WordLessons).ThenInclude(wl => wl.UserWordLessons.Where(uwl => uwl.UserId == userId))
                .Include(x => x.Stories).ThenInclude(s => s.Sentences)
                .Include(x => x.Stories).ThenInclude(s => s.StoryPoint)
                .Include(x => x.Stories).ThenInclude(x => x.Quiz!)
                .Include(x => x.Stories).ThenInclude(s => s.UserStories.Where(us => us.UserId == userId))
                .Include(x => x.Stories.OrderBy(s => s.OrderInSeason)).FirstOrDefault();
        }
    }
}
