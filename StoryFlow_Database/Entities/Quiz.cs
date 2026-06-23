namespace StoryFlow_Database.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public Story? Story { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
