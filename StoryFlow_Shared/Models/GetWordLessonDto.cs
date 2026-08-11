namespace StoryFlow_Shared.Models
{
    public class GetWordLessonDto
    {
        public int Id { get; set; }
        public int? OrderInSeason { get; set; }
        public int? StorySeasonId { get; set; }
        public string? PolishTitlte { get; set; }
        public List<GetWordDto> Words { get; set; } = new();
    }
}
