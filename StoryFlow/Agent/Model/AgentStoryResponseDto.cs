using StoryFlow_Shared.Enums;

namespace StoryFlow.Agent.Model
{
    public class AgentStoryResponseDto
    {
        public string PolishStory { get; set; } = string.Empty;
        public string EnglishStory { get; set; } = string.Empty;    
        public string PolishTitle { get; set; } = string.Empty;
        public string EnglishTitle { get; set; } = string.Empty;
        public string PolishDescription { get; set; } = string.Empty;
        public string EnglishDescription { get; set; } = string.Empty;
        public StoryCategory? StoryCategory { get; set; }
        public LanguageLevel? LanguageLevel { get; set; }
    }
}
