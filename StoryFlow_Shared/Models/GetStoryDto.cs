using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class GetStoryDto
    {
        public int Id { get; set; }
        public string? PolishStory { get; set; }
        public string? EnglishStory { get; set; }
        public string? PolishTitle { get; set; }
        public string? EnglishTitle { get; set; }
        public string? PolishDescription { get; set; }
        public string? EnglishDescription { get; set; }
        public int MaxPoints { get; set; }
        public StoryCategory? StoryCategory { get; set; }
        public StorySize? StorySize { get; set; }
        public LanguageLevel? LanguageLevel { get; set; }
        public ICollection<GetSentenceDto> Sentences { get; set; } = new List<GetSentenceDto>();
        public ICollection<GetUserStoryDto> UserStories { get; set; } = new List<GetUserStoryDto>();
        public GetQuizDto? Quiz { get; set; }
    }
}
