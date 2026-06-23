namespace StoryFlow_Database.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
