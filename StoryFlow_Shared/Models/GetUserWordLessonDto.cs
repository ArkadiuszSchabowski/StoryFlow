namespace StoryFlow_Shared.Models
{
    public class GetUserWordLessonDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WordLessonId { get; set; }
        public int BestResult { get; set; } = 0;
        public double PercentageScore { get; set; }
    }
}
