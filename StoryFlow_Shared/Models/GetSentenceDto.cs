namespace StoryFlow_Shared.Models
{
    public class GetSentenceDto
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public int? Order { get; set; }
        public string PolishMeaning { get; set; } = string.Empty;
        public string EnglishMeaning { get; set; } = string.Empty;
        public ICollection<GetUserSentenceDto> UserSentences { get; set; } = new List<GetUserSentenceDto>();
    }
}
