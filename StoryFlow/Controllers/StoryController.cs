using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IService _service;

        public StoryController(IService service)
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

        [HttpPost]
        public async Task<IActionResult> Add(AddStoryDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            _service.Remove(id);
            return NoContent();
        }
    }
}
