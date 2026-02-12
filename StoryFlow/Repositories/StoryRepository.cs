using Microsoft.EntityFrameworkCore;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
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

        public async Task<Story> Get(int id)
        {
            return await _context.Stories.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ICollection<Story>> GetAll()
        {
            return await _context.Stories.Include(s => s.Sentences).ToListAsync();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
