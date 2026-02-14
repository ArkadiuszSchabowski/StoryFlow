using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class AddStoryDto
    {
        public string? PolishStory { get; set; }
        public string? EnglishStory {  get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StoryCategory? StoryCategory { get; set; }
        public StorySize? StorySize { get; set; }
        public LanguageLevel? LanguageLevel { get; set; }
    }
}
