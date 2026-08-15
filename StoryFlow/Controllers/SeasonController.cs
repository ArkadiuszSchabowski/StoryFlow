using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;
using System.Security.Claims;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonService _service;

        public SeasonController(ISeasonService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("{seasonId}")]
        public async Task<ActionResult<GetStorySeasonDto>> GetBySeason([FromRoute] int seasonId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            GetStorySeasonDto season = await _service.GetBySeason(seasonId, userIdClaim);
            return Ok(season);
        }
    }
}
