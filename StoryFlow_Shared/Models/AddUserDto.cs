using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class AddUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword {  get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth {  get; set; }
    }
}
