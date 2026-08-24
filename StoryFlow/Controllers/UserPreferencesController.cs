using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using System.Security.Claims;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUserPreferencesService _service;

        public UserPreferencesController(IUserPreferencesService service)
        {
            _service = service;
        }

        [HttpPost("theme")]
        public async Task<ActionResult> SetTheme([FromBody] bool theme)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            await _service.SetTheme(userId, theme);

            return Ok();
        }
    }
}
