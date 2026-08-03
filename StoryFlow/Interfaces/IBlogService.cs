using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IBlogService
    {
        Task<List<GetBlogPostDto>> Get();
        Task<GetBlogPostDto> GetBySlug(string slug);
    }
}
