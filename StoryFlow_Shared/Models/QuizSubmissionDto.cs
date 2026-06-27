namespace StoryFlow_Shared.Models
{
    public class QuizSubmissionDto
    {
        public int StoryId { get; set; }
        public List<AnswerDto> Answers { get; set; } = [];
    }
}
