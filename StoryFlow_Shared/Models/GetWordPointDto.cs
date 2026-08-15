namespace StoryFlow_Shared.Models
{
    public class GetWordPointDto
    {
        public int Id { get; set; }
        public int WordLessonId { get; set; }
        public int? MaxPoints { get; set; }
        public int? PointsPerWord { get; set; }
    }
}
