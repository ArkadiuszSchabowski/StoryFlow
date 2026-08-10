using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GetBlogPostDto>> Get()
        {
            List<GetBlogPostDto> blogPosts = await _service.Get();

            return Ok(blogPosts);
        }

        [HttpGet("visible")]
        public async Task<ActionResult<GetBlogPostDto>> GetVisibleBlogPosts()
        {
            List<GetBlogPostDto> blogPosts = await _service.GetVisibleBlogPosts();

            return Ok(blogPosts);
        }

        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult<GetBlogPostDto>> GetBySlug([FromRoute] string slug)
        {
            GetBlogPostDto blogPost = await _service.GetBySlug(slug);

            return Ok(blogPost);
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddBlogPostDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost("generate")]
        public async Task<ActionResult> Generate([FromBody] GenerateBlogPostDto dto)
        {
            return Ok(await _service.Generate(dto));
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost("section")]
        public async Task<ActionResult> AddBlogPostSection([FromBody] AddBlogPostSectionDto dto)
        {
            await _service.AddBlogPostSection(dto);
            return Ok();
        }
    }
}
