using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MyDbContext _context;

        public BlogRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<BlogPost>> Get()
        {
            return await _context.BlogPosts.OrderByDescending(x => x.OrderInBlog).ToListAsync();
        }

        public async Task<BlogPost?> GetBySlug(string slug)
        {
            return await _context.BlogPosts.Include(x => x.Sections.OrderBy(s => s.Order)).FirstOrDefaultAsync(x => x.Slug == slug);
        }
    }
}
