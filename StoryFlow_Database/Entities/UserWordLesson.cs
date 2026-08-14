namespace StoryFlow_Database.Entities
{
    public class UserWordLesson
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int WordLessonId { get; set; }
        public WordLesson? WordLesson { get; set; }
        public int BestResult { get; set; } = 0;
        public double PercentageScore { get; set; }
    }
}
