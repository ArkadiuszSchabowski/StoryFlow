namespace StoryFlow_Shared.Models
{
    public class AddSentenceDto
    {
        public int StoryId { get; set; }
        public int? Order { get; set; }
        public string PolishMeaning { get; set; } = string.Empty;
        public string EnglishMeaning { get; set; } = string.Empty;
    }
}
