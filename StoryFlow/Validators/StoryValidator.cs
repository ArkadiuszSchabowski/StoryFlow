using StoryFlow.Exceptions;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Validators
{
    public class StoryValidator : IValidator<AddStoryDto>
    {
        public void Validate(AddStoryDto? dto)
        {
            if(dto == null)
            {
                throw new BadRequestException("Dto is required.");
            }
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                throw new BadRequestException("Title is required.");
            }

            if (dto.Title.Length < 5 || dto.Title.Length > 50)
            {
                throw new BadRequestException("Title must be between 5 and 50 characters long.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishStory))
            {
                throw new BadRequestException("Story is required.");
            }

            if (dto.EnglishStory.Length < 150 || dto.EnglishStory.Length > 1000)
            {
                throw new BadRequestException("Story must be between 150 and 1000 characters long.");
            }
        }
    }
}
