namespace StoryFlow_Shared.Models
{
    public class GetQuestionDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public ICollection<GetAnswerDto> Answers { get; set; } = new List<GetAnswerDto>();
    }
}
