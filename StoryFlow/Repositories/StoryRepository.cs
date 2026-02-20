using Microsoft.EntityFrameworkCore;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Repositories
{
    public class StoryRepository : IRepository<Story>
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

        public async Task<ICollection<Story>> Get(LanguageLevel? languageLevel, StoryCategory? category, StorySize? size)
        {
            var query = _context.Stories.Include(s => s.Sentences).AsQueryable();

            if (languageLevel.HasValue)
                query = query.Where(x => x.LanguageLevel == languageLevel.Value);

            if (category.HasValue)
                query = query.Where(x => x.StoryCategory == category.Value);

            if (size.HasValue)
                query = query.Where(x => x.StorySize == size.Value);

            return await query.ToListAsync();
        }


        public void Remove(Story story)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
