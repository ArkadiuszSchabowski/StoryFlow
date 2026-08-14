namespace StoryFlow_Shared.Models
{
    public class GetWordLessonDto
    {
        public int Id { get; set; }
        public int? OrderInSeason { get; set; }
        public int? StorySeasonId { get; set; }
        public string? PolishTitlte { get; set; }
        public string? Image { get; set; }
        public List<GetWordDto> Words { get; set; } = new();
        public GetWordPointDto? WordPoint { get; set; }
        public List<GetUserWordLessonDto> UserWordLessons { get; set; } = new();
    }
}
