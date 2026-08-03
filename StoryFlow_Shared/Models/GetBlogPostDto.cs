namespace StoryFlow_Shared.Models
{
    public class GetBlogPostDto
    {
        public int Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string MetaTitleContent { get; set; } = string.Empty;

        public string MetaTitleDescription { get; set; } = string.Empty;
        public string H2Title { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? Url { get; set; }
        public string? UrlText { get; set; }
        public string? Summary { get; set; }

        public DateTime? PublishedAt { get; set; }

        public int OrderInBlog { get; set; }

        public List<GetBlogPostSectionDto> Sections { get; set; } = new();
    }
}
