namespace StoryFlow_Database.Entities
{
    public class WordLesson
    {
        public int Id { get; set; }
        public int? OrderInSeason { get; set; }
        public int? StorySeasonId { get; set; }
        public string? PolishTitlte { get; set; }
        public StorySeason? StorySeason { get; set; }
        public List<Word> Words { get; set; } = new ();
        public WordPoint? WordPoint { get; set;  }
    }
}
