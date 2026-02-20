using Microsoft.EntityFrameworkCore;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Repositories
{
    public class StoryRepository : IStoryRepository
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

        public async Task<ICollection<Story>> GetAll()
        {
            return await _context.Stories.Include(s => s.Sentences).ToListAsync();
        }

        public async Task<ICollection<Story>> Get(StoryFilter filter)
        {
            var query = _context.Stories.Include(s => s.Sentences).AsQueryable();

            if (filter.LanguageLevel.HasValue)
                query = query.Where(x => x.LanguageLevel == filter.LanguageLevel.Value);

            if (filter.Category.HasValue)
                query = query.Where(x => x.StoryCategory == filter.Category.Value);

            if (filter.Size.HasValue)
                query = query.Where(x => x.StorySize == filter.Size.Value);

            return await query.ToListAsync();
        }


        public Task Remove(Story story)
        {
            throw new NotImplementedException();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
