namespace StoryFlow_Shared.Models
{
    public class AddQuizDto
    {
        public int StoryId { get; set; }
        public ICollection<AddQuestionDto> Questions { get; set; } = new List<AddQuestionDto>();
    }
}
