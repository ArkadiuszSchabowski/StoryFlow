using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Nick { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword {  get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth {  get; set; }
        public int RoleId { get; set; }
    }
}
