namespace StoryFlow_Shared.Models
{
    public class AddQuestionDto
    {
        public int QuizId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public ICollection<AddAnswerDto> Answers { get; set; } = new List<AddAnswerDto>();
    }
}
