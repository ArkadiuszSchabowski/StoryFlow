namespace StoryFlow_Database.Entities
{
    public class Sentence
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public Story? Story { get; set; }
        public int Order { get; set; }
        public string PolishMeaning { get; set; } = string.Empty;
        public string EnglishMeaning { get; set; } = string.Empty;
        public ICollection<UserSentence> UserSavedSentences { get; set; } = new List<UserSentence>(); 
    }
}
