namespace StoryFlow_Shared.Models
{
    public class AddBlogPostSectionDto
    {
        public int? BlogPostId { get; set; }
        public string? H3Title { get; set; }
        public string? Text { get; set; }
        public int? Order { get; set; }
    }
}
