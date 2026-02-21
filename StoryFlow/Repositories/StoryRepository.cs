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
            return _context.Stories.Include(s => s.Sentences).AsQueryable();
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
