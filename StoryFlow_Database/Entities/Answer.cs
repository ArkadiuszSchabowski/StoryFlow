namespace StoryFlow_Database.Entities
{
        public class Answer
        {
            public int Id { get; set; }

            public int QuestionId { get; set; }
            public Question? Question { get; set; }

            public string Key { get; set; } = string.Empty;

            public string Text { get; set; } = string.Empty;

            public bool IsCorrect { get; set; }
        }
}
