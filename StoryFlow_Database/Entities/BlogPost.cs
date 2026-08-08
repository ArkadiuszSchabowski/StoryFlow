namespace StoryFlow_Database.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string MetaTitleContent { get; set; } = string.Empty;

        public string MetaTitleDescription { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? Url { get; set; }
        public string? UrlText { get; set; }
        public string? Summary { get; set; }
        public bool? IsVisible { get; set; } = false;

        public DateTime? PublishedAt { get; set; }

        public List<BlogPostSection> Sections { get; set; } = new();
    }
}
