using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class AddStoryDto
    {
        public string? PolishStory { get; set; }
        public string? EnglishStory {  get; set; }
        public string? PolishTitle { get; set; }
        public string? EnglishTitle { get; set; }
        public string? PolishDescription { get; set; }
        public string? EnglishDescription { get; set; }
        public StoryCategory? StoryCategory { get; set; }
        public LanguageLevel? LanguageLevel { get; set; }
        public AddQuizDto? Quiz { get; set; }
    }
}
