namespace StoryFlow_Shared.Models
{
    public class AddQuestionDto
    {
        public string QuestionText { get; set; } = string.Empty;
        public ICollection<AddAnswerDto> Answers { get; set; } = new List<AddAnswerDto>();
    }
}
