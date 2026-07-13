namespace StoryFlow_Shared.Models
{
    public class GetStorySeasonDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SeasonNumber { get; set; }
        public bool IsVisibleForUser { get; set; }
        public List<GetStoryDto> Stories { get; set; } = new();
        public int? MaxPoints { get; set; }
    }
}
