namespace StoryFlow_Shared.Models
{
    public class GetWordDto
    {
        public int Id { get; set; }
        public string? PolishWord { get; set; }
        public string? EnglishWord { get; set; }
        public string? ImageUrl { get; set; }
        public string? HintSentence { get; set; }
    }
}
