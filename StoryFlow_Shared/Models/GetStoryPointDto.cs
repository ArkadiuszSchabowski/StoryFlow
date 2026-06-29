namespace StoryFlow_Shared.Models
{
    public class GetStoryPointDto
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public int? MaxPoints { get; set; }
        public int? PointsPerAnswer { get; set; }
        public int? BonusPointsForStoryLength { get; set; }
    }
}
