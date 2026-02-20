using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class StoryFilter
    {
        public LanguageLevel? LanguageLevel { get; set; }
        public StoryCategory? Category { get; set; }
        public StorySize? Size { get; set; }
    }
}
