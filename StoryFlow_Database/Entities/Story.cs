using StoryFlow_Shared.Enums;

namespace StoryFlow_Database.Entities
{
    public class Story
    {
        public int Id { get; set; }
        public string? PolishStory { get; set; }
        public string? EnglishStory { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public StoryCategory? StoryCategory { get; set; }
        public StorySize? StorySize {get; set;}
        public LanguageLevel? LanguageLevel { get; set; }
        public ICollection<Sentence> Sentences { get; set; } = new List<Sentence>();
        public ICollection<UserStory> UserStories { get; set; } = new List<UserStory>();
    }
}
