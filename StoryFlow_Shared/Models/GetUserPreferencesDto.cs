namespace StoryFlow_Shared.Models
{
    public class GetUserPreferencesDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool isLightTheme { get; set; }
    }
}
