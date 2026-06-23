namespace StoryFlow_Shared.Models
{
    public class GetQuizDto
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public ICollection<GetQuestionDto> Questions { get; set; } = new List<GetQuestionDto>();
    }
}
