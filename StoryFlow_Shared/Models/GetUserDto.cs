using Microsoft.AspNetCore.Identity;

namespace StoryFlow_Shared.Models
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public int? PremiumAccountDays { get; set; }
    }
}
