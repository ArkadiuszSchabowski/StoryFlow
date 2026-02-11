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
        [HttpPost]
        public async Task<IActionResult> Add(AddStoryDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }
    }
}
