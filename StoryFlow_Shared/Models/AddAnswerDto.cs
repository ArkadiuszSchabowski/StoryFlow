namespace StoryFlow_Shared.Models
{
    public class AddAnswerDto
    {
        public int QuestionId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
