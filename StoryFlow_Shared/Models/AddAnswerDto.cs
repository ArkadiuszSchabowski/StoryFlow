namespace StoryFlow_Shared.Models
{
    public class AddAnswerDto
    {
        public string Key { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
