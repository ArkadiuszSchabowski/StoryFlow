using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IBlogRepository
    {
        Task<ICollection<BlogPost>> Get();
        Task<BlogPost?> GetBySlug(string slug);
    }
}
