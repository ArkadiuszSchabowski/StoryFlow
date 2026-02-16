using Microsoft.AspNetCore.Mvc;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentenceController : ControllerBase
    {
        private readonly ISentenceService _service;

        public SentenceController(ISentenceService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSentenceDto>> Get(int id)
        {
            var sentence = await _service.Get(id);
            return Ok(sentence);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetSentenceDto>>> GetAll()
        {
            var sentences = await _service.GetAll();
            return Ok(sentences);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddSentenceDto dto)
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