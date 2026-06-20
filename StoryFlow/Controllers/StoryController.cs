using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

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

        [HttpGet]
        public async Task<ActionResult<GetStoryDto>> Get([FromQuery] StoryFilter? dto)
        {
            var stories = await _service.Get(dto!);

            return Ok(stories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStoryDto>> Get(int id) 
        {
            var story = await _service.Get(id);
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
