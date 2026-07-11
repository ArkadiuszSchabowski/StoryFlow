using StoryFlow_Shared.Enums;

namespace StoryFlow_Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public ICollection<UserHobby> UserHobbies { get; set; } = new List<UserHobby>();
        public ICollection<UserSentence> UserSentences { get; set; } = new List<UserSentence>();
        public ICollection<UserStory> UserStories { get; set; } = new List<UserStory>();
        public int Stars { get; set; } = 0;
        public int Tickets { get; set; } = 3;
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public int PremiumAccountDays { get; set; } = 0;
    }
}
