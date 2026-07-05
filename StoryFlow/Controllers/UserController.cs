using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;
using System.Security.Claims;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<GetUserDto>> Get()
        {
            var users = await _service.Get();

            return Ok(users);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<GetUserDto>> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var user = await _service.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            return Ok(user);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> Get(int id)
        {
            var user = await _service.Get(id);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto dto)
        {
            var token = await _service.Login(dto);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Add([FromBody] RegisterUserDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return NoContent();
        }


    }
}
