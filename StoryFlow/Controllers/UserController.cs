using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

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

        //admin
        [HttpGet]
        public async Task<ActionResult<GetUserDto>> Get()
        {
            var users = await _service.Get();

            return Ok(users);
        }

        //admin
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
        public async Task<ActionResult> Add(RegisterUserDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        //admin
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return NoContent();
        }


    }
}
