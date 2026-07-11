using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;
using System.Security.Claims;

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

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddQuizDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost("generate")]
        public async Task<ActionResult> Generate([FromBody] GenerateQuizDto dto)
        {
            return Ok(await _service.Generate(dto));
        }

        [Authorize]
        [HttpPost("check")]
        public async Task<ActionResult<QuestionSubmissionResult>> Check(
            [FromBody] QuestionSubmissionDto dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            QuestionSubmissionResult result = await _service.CheckAnswer(dto, userId!);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("submit")]
        public async Task<ActionResult> Submit([FromBody] QuizSubmissionDto dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            QuizResult quizResult = await _service.CheckAnswers(dto, userId);
            return Ok(quizResult);
        }
    }
}
