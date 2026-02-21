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

        [HttpGet]
        public async Task<ActionResult<GetUserDto>> Get()
        {
            var users = await _service.Get();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> Get(int id)
        {
            var user = await _service.Get(id);
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult> Add(AddUserDto dto)
        {
            await _service.Add(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _service.Remove(id);
            return NoContent();
        }


    }
}
