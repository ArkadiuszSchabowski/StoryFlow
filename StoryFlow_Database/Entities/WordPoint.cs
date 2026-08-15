namespace StoryFlow_Database.Entities
{
    public class WordPoint
    {
        public int Id { get; set; }
        public int WordLessonId { get; set; }
        public WordLesson? WordLesson { get; set; }
        public int? MaxPoints { get; set; }
        public int? PointsPerWord { get; set; }
    }
}
