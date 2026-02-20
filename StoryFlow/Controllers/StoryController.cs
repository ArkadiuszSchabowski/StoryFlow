using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Enums;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStoryDto>> Get(int id) 
        {
            var story = await _service.Get(id);
            return Ok(story);
        }


        [HttpGet]
        public async Task<ActionResult<List<GetStoryDto>>> GetAll()
        {
            var stories = await _service.GetAll();
            return Ok(stories);
        }
        [HttpGet("filtered")]
        public async Task<ActionResult<GetStoryDto>> Get(LanguageLevel? languageLevel, StoryCategory? category, StorySize? size)
        {
            var stories = await _service.Get(languageLevel, category, size);

            return Ok(stories);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddStoryDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return NoContent();
        }
    }
}
