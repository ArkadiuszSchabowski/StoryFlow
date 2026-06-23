using StoryFlow_Shared.Enums;

namespace StoryFlow_Database.Entities
{
    public class Story
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
        public StorySize? StorySize {get; set;}
        public LanguageLevel? LanguageLevel { get; set; }
        public ICollection<Sentence> Sentences { get; set; } = new List<Sentence>();
        public ICollection<UserStory> UserStories { get; set; } = new List<UserStory>();
        public Quiz? Quiz { get; set; }
    }
}
