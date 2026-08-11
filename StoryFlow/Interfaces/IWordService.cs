using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IWordService
    {
        Task<GetWordLessonDto> Get(int id, string? userIdClaim);
    }
}
