namespace StoryFlow_Shared.Models
{
    public class AddBlogPostDto
    {
        public string? Slug { get; set; }
        public string? MetaTitleContent { get; set; }
        public string? MetaTitleDescription { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? Summary { get; set; }
        public string? Url { get; set; }
        public string? UrlText { get; set; }
    }
}
