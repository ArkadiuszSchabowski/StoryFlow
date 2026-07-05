using StoryFlow.Exceptions;
using StoryFlow.Interfaces.Validators;
using StoryFlow_Shared.Models;
using System.Text.RegularExpressions;

namespace StoryFlow.Validators
{
    public class UserValidator : IUserValidator
    {
        public void ValidateDto(RegisterUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new BadRequestException("Adres e-mail nie może być pusty.");

            if (!Regex.IsMatch(dto.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)+$"))
                throw new BadRequestException("Nieprawidłowy format adresu e-mail.");

            if (string.IsNullOrWhiteSpace(dto.FirstName) || dto.FirstName.Length < 3 || dto.FirstName.Length > 25)
            {
                throw new BadRequestException("Imię musi mieć od 3 do 25 znaków.");
            }

            if (string.IsNullOrWhiteSpace(dto.LastName) || dto.LastName.Length < 3 || dto.LastName.Length > 25)
            {
                throw new BadRequestException("Nazwisko musi mieć od 3 do 25 znaków.");
            }

            if (dto.Password != dto.RepeatPassword)
            {
                throw new BadRequestException("Hasła nie są takie same.");
            }

            if ((dto.Password.Length < 5 || dto.RepeatPassword.Length < 5) || (dto.Password.Length > 25 || dto.RepeatPassword.Length > 25))
            {
                throw new BadRequestException("Hasło musi mieć od 5 do 25 znaków.");
            }

            if (dto.DateOfBirth == null)
            {
                throw new BadRequestException("Data urodzenia jest wymagana.");
            }

            if (dto.DateOfBirth < DateOnly.Parse("1900-01-01") || dto.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new BadRequestException("Nieprawidłowa data urodzenia.");
            }

            if (dto.Gender == null)
            {
                throw new BadRequestException("Płeć jest wymagana.");
            }
        }
    }
}