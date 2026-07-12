namespace StoryFlow_Shared.Models
{
    public class GetStorySeasonDto
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public bool IsVisibleForUser { get; set; }
        public List<GetStoryDto> Stories { get; set; } = new();
    }
}
