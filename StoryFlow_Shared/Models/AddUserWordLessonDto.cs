namespace StoryFlow_Shared.Models
{
    public class AddUserWordLessonDto
    {
        public int UserId { get; set; }
        public int WordLessonId { get; set; }
        public int BestResult { get; set; } = 0;
    }
}
