namespace StoryFlow_Database.Entities
{
    public class UserStory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int StoryId { get; set; }
        public Story? Story { get; set; }
        public int BestResult { get; set; } = 0;
        public bool HasReceivedStoryLengthBonus { get; set; } = false;
    }
}
