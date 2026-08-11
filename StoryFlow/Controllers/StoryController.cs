using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;
using System.Security.Claims;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _service;

        public StoryController(IStoryService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetStoryDto>> Get([FromQuery] StoryFilter? dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var stories = await _service.Get(dto!, userId);

            return Ok(stories);
        }

        [Authorize]
        [HttpGet("season")]
        public async Task<ActionResult<List<GetStorySeasonDto>>> GetSeasons()
        {
            string? userId = (User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            List<GetStorySeasonDto> seasons = await _service.GetSeasons(userId);

            return Ok(seasons);
        }

        [Authorize]
        [HttpGet("{storyId}")]
        public async Task<ActionResult<GetStoryDto>> Get([FromRoute] int storyId) 
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var story = await _service.Get(storyId, userIdClaim);
            return Ok(story);
        }

        [HttpGet("welcome")]
        public async Task<ActionResult<GetStoryDto>> GetIntroStory()
        {
            GetStoryDto story = await _service.GetWelcomeStory();
            return Ok(story);
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddStoryDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost("generate")]
        public async Task<ActionResult> Generate([FromBody] GenerateStoryDto dto)
        {
            return Ok(await _service.Generate(dto));
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return NoContent();
        }
    }
}
