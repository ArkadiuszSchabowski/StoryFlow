using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class GenerateStoryDto
    {
        public StorySize StorySize {get; set;}
        public StoryCategory StoryCategory { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
    }
}
