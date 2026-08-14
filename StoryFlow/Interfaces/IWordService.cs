using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IWordService
    {
        Task<GetWordLessonDto> Get(int id, string? userIdClaim);
        Task SaveBestResult(int wordLessonId, string? userIdClaim, int result);
    }
}
