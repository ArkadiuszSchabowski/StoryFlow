namespace StoryFlow_Database.Entities
{
    public class StoryPoint
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public Story? Story { get; set; }
        public int? MaxPoints { get; set; }
        public int? PointsPerAnswer { get; set; }
        public int? BonusPointsForStoryLength { get; set; }
    }
}
