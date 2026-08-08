using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IBlogValidator
    {
        public void Validate(AddBlogPostDto? dto);
        public void ValidateBlogPostSection(AddBlogPostSectionDto? dto);
    }
}
