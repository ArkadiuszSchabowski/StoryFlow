namespace StoryFlow_Database.Entities
{
    public class StorySeason
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public bool IsVisibleForUser { get; set; } = false;
        public List<Story> Stories { get; set; } = new();
    }
}
