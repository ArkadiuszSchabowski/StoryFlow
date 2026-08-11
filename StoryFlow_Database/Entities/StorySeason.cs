namespace StoryFlow_Database.Entities
{
    public class StorySeason
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SeasonNumber { get; set; }
        public bool IsVisibleForUser { get; set; } = false;
        public List<Story> Stories { get; set; } = new();
        public List<WordLesson> WordLessons {get; set; } = new ();
        public int? MaxPoints { get; set; }
        public int? PointsRequiredToUnlock { get; set; }
    }
}
