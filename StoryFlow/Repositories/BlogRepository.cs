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
            return await _context.BlogPosts.OrderByDescending(x => x.PublishedAt).ToListAsync();
        }

        public async Task<ICollection<BlogPost>> GetVisibleBlogPosts()
        {
            return await _context.BlogPosts.Where(x => x.IsVisible == true).OrderByDescending(x => x.PublishedAt).ToListAsync();
        }

        public async Task<BlogPost?> GetBySlug(string slug)
        {
            return await _context.BlogPosts.Include(x => x.Sections.OrderBy(s => s.Order)).FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task Add(BlogPost entity)
        {
            await _context.BlogPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddBlogPostSection(BlogPostSection entity)
        {
            await _context.BlogPostSections.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
