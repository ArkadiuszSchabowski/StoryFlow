using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IBlogService
    {
        Task<List<GetBlogPostDto>> Get();
        Task<List<GetBlogPostDto>> GetVisibleBlogPosts();
        Task<GetBlogPostDto> GetBySlug(string slug);
        Task Add(AddBlogDto dto);
    }
}
