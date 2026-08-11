using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;
using System.Security.Claims;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService _service;

        public WordController(IWordService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet("{wordLessonId}")]
        public async Task<ActionResult<GetStoryDto>> Get([FromRoute] int wordLessonId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var wordLesson = await _service.Get(wordLessonId, userIdClaim);
            return Ok(wordLesson);
        }

    }
}
