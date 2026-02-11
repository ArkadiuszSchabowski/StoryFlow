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

        public Story Get(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Story> GetAll()
        {
            throw new NotImplementedException();
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
