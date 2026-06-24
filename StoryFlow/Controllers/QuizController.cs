using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddQuizDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }
    }
}
