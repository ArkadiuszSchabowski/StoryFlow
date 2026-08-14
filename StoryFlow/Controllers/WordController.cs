using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
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
        [HttpGet("lesson/{wordLessonId}")]
        public async Task<ActionResult<GetStoryDto>> Get([FromRoute] int wordLessonId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var wordLesson = await _service.Get(wordLessonId, userIdClaim);
            return Ok(wordLesson);
        }

        [Authorize]
        [HttpPost("lesson/save/{wordLessonId}")]
        public async Task SaveBestResult([FromRoute] int wordLessonId,[FromBody] int result)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _service.SaveBestResult(wordLessonId, userIdClaim, result);
        }

    }
}
