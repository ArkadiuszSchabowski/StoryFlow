using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Models
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? Nick { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int? Stars { get; set; }
        public int Tickets { get; set; }
        public ICollection<GetHobbyDto> Hobbies { get; set; } = new List<GetHobbyDto>();
        public GetUserPreferencesDto? UserPreferences { get; set; }
    }
}
