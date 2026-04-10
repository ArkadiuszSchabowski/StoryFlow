using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IAIStoryService
    {
        public Task<string?> GenerateStoryByUserHobby(int userId);
    }
}
