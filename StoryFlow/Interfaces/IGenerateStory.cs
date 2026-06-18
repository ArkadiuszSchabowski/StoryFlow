using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IGenerateStory
    {
        public Task<string> Generate(GenerateStoryDto dto);
    }
}
