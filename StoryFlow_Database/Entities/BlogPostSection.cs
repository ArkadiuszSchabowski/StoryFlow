namespace StoryFlow_Database.Entities
{
    public class BlogPostSection
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        public string? H3Title { get; set; }
        public string? Text { get; set; }
        public int? Order { get; set; }
    }
}
