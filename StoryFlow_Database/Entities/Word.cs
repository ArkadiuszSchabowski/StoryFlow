namespace StoryFlow_Database.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public int WordLessonId { get; set; }
        public WordLesson? WordLesson { get; set; }
        public string? PolishWord { get; set; }
        public string? EnglishWord { get; set; }
    }
}
