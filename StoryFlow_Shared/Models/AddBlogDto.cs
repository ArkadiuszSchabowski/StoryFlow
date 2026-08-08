namespace StoryFlow_Shared.Models
{
    public class AddBlogDto
    {
        public string Slug = string.Empty;
        public string MetaTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? Url { get; set; }
        public string? UrlText { get; set; }
        public DateOnly PublishedAt { get; set; }
    }
}
