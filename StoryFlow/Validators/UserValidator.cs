using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;
using System.Text.RegularExpressions;

namespace StoryFlow.Validators
{
    public class UserValidator : IUserValidator
    {
        public void ValidateDto(AddUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new BadRequestException("Email address cannot be empty.");

            if (!Regex.IsMatch(dto.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)+$"))
                throw new BadRequestException("Invalid email address format.");

            if (string.IsNullOrWhiteSpace(dto.FirstName) || dto.FirstName.Length < 3 || dto.FirstName.Length > 25)
            {
                throw new BadRequestException("User first name must be between 3 and 25 characters.");
            }

            if (string.IsNullOrWhiteSpace(dto.LastName) || dto.LastName.Length < 3 || dto.LastName.Length > 25)
            {
                throw new BadRequestException("User last name must be between 3 and 25 characters.");
            }

            if (dto.Password != dto.RepeatPassword)
            {
                throw new BadRequestException("Passwords are not the same.");
            }

            if ((dto.Password.Length < 5 || dto.RepeatPassword.Length < 5) || (dto.Password.Length > 25 || dto.RepeatPassword.Length > 25))
            {
                throw new BadRequestException("Password must be between 5 and 25 characters.");
            }

            if (dto.DateOfBirth < DateOnly.Parse("1900-01-01") || dto.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new BadRequestException("Incorrect date of birth.");
            }
        }
    }
}
