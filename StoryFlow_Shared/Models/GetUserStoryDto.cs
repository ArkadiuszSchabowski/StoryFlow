namespace StoryFlow_Shared.Models
{
    public class GetUserStoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoryId { get; set; }
        public int BestResult { get; set; }
        public double PercentageScore { get; set; }
    }
}
