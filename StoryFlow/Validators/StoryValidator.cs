using StoryFlow.Exceptions;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Validators
{
    public class StoryValidator : IValidator<AddStoryDto>
    {
        public void Validate(AddStoryDto? dto)
        {
            if(dto is null)
            {
                throw new BadRequestException("Dto is required.");
            }
            if (string.IsNullOrWhiteSpace(dto.PolishTitle))
            {
                throw new BadRequestException("Polish title is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishTitle))
            {
                throw new BadRequestException("English title is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.PolishDescription))
            {
                throw new BadRequestException("Polish description is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishDescription))
            {
                throw new BadRequestException("English description is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishStory))
            {
                throw new BadRequestException("English story is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.PolishStory))
            {
                throw new BadRequestException("Polish story is required.");
            }

            if (dto.EnglishTitle.Length < 5 || dto.EnglishTitle.Length > 50)
            {
                throw new BadRequestException("English title must be between 5 and 50 characters long.");
            }

            if (dto.EnglishDescription.Length < 10 || dto.EnglishDescription.Length > 100)
            {
                throw new BadRequestException("English description must be between 10 and 100 characters long.");
            }

            if (dto.EnglishStory.Length < 400 || dto.EnglishStory.Length > 1500)
            {
                throw new BadRequestException("English story must be between 400 and 1500 characters long.");
            }
        }
        public void ValidateSentencesCount(int polishSentencesCount, int englishSentencesCount)
        {
            if (polishSentencesCount != englishSentencesCount)
            {
                throw new BadRequestException("Polish sentences are not equal to english sentences.");
            }
        }
    }
}
