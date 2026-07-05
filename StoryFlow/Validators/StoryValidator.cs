using StoryFlow.Exceptions;
using StoryFlow.Interfaces.Validators;
using StoryFlow_Shared.Models;

namespace StoryFlow.Validators
{
    public class StoryValidator : IValidator<AddStoryDto>
    {
        public void Validate(AddStoryDto? dto)
        {
            if(dto is null)
            {
                throw new BadRequestException("Encja jest wymagana.");
            }
            if (string.IsNullOrWhiteSpace(dto.PolishTitle))
            {
                throw new BadRequestException("Polski tytuł jest wymagany.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishTitle))
            {
                throw new BadRequestException("Angielski tytuł jest wymagany.");
            }

            if (string.IsNullOrWhiteSpace(dto.PolishDescription))
            {
                throw new BadRequestException("Polski opis jest wymagany.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishDescription))
            {
                throw new BadRequestException("Angielski opis jest wymagany.");
            }

            if (string.IsNullOrWhiteSpace(dto.EnglishStory))
            {
                throw new BadRequestException("Angielska historia jest wymagana.");
            }

            if (string.IsNullOrWhiteSpace(dto.PolishStory))
            {
                throw new BadRequestException("Polska historia jest wymagana.");
            }

            if (dto.EnglishTitle.Length < 5 || dto.EnglishTitle.Length > 50)
            {
                throw new BadRequestException("Tytuł w języku angielskim musi mieć od 5 do 50 znaków.");
            }

            if (dto.EnglishDescription.Length < 10 || dto.EnglishDescription.Length > 100)
            {
                throw new BadRequestException("Opis w języku angielskim musi mieć od 10 do 100 znaków.");
            }

            if (dto.EnglishStory.Length < 400 || dto.EnglishStory.Length > 1500)
            {
                throw new BadRequestException("Historia w języku angielskim musi mieć od 400 do 1500 znaków.");
            }
        }
        public void ValidateSentencesCount(int polishSentencesCount, int englishSentencesCount)
        {
            if (polishSentencesCount != englishSentencesCount)
            {
                throw new BadRequestException("Liczba zdań w języku polskim różni się od liczby zdań w języku angielskim.");
            }
        }
    }
}
