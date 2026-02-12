using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class GetStoryDto
    {
        public int Id { get; set; }
        public string? PolishStory { get; set; }
        public string? EnglishStory { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public StoryCategory? StoryCategory { get; set; }
        public StorySize? StorySize { get; set; }
        public LanguageLevel? LanguageLevel { get; set; }
        public ICollection<GetSentenceDto> Sentences { get; set; } = new List<GetSentenceDto>();
        public ICollection<GetUserStoryDto> UserStories { get; set; } = new List<GetUserStoryDto>();
    }
}
