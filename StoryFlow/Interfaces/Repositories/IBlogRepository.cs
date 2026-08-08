using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        Task<ICollection<BlogPost>> Get();
        Task<ICollection<BlogPost>> GetVisibleBlogPosts();
        Task<BlogPost?> GetBySlug(string slug);
        Task Add(BlogPost entity);
        Task AddBlogPostSection(BlogPostSection entity);
    }
}
