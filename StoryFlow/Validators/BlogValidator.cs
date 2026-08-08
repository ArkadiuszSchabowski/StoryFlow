using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Validators
{
    public class BlogValidator : IBlogValidator
    {
        private const int MetaTitleMaxLength = 50;
        private const int MetaDescriptionMaxLength = 115;
        private const int H3TitleMaxLength = 70;

        public void Validate(AddBlogPostDto? dto)
        {
            if (dto is null)
            {
                throw new BadRequestException("Encja jest wymagana.");
            }

            if(dto.Slug is null)
            {
                throw new BadRequestException("Slug jest wymagany.");
            }

            if(dto.MetaTitleContent is null)
            {
                throw new BadRequestException("Tytuł jest wymagany.");
            }

            if (dto.MetaTitleDescription is null)
            {
                throw new BadRequestException("Opis jest wymagany.");
            }

            if (dto.MetaTitleContent.Length > MetaTitleMaxLength)
            {
                throw new BadRequestException($"Tytuł nie może przekraczać {MetaTitleMaxLength} znaków.");
            }

            if (dto.MetaTitleDescription.Length > MetaDescriptionMaxLength)
            {
                throw new BadRequestException($"Opis nie może przekraczać {MetaDescriptionMaxLength} znaków.");
            }
        }

        public void ValidateBlogPostSection(AddBlogPostSectionDto? dto)
        {
            if (dto is null)
            {
                throw new BadRequestException("Encja jest wymagana.");
            }

            if (dto.H3Title is null)
            {
                throw new BadRequestException("Tytuł nagłówka jest wymagany.");
            }

            if (dto.H3Title.Length > H3TitleMaxLength)
            {
                throw new BadRequestException($"Tytuł nie może przekraczać {H3TitleMaxLength} znaków.");
            }

            if (dto.Text is null)
            {
                throw new BadRequestException("Opis nagłówka jest wymagany.");
            }

            if(dto.BlogPostId is null)
            {
                throw new BadRequestException("Id posta jest wymagane.");
            }
        }
    }
}
