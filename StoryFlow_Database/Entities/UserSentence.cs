namespace StoryFlow_Database.Entities
{
    public class UserSentence
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int SentenceId { get; set; }
        public Sentence? Sentence { get; set; }
    }
}
