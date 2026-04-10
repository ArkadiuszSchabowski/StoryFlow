using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;
using System.Security.Claims;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIStoryController : ControllerBase
    {
        private readonly IAIStoryService _service;

        public AIStoryController(IAIStoryService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GenerateStoryByUserHobby()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                int id = int.Parse(userId!);
                string? text = await _service.GenerateStoryByUserHobby(id);

                 return Ok(text);
            }

            return Ok();

        }
    }
}
