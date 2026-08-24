namespace StoryFlow_Database.Entities
{
    public class UserPreferences
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public  User? User { get; set; }
        public bool isLightTheme { get; set; } = false;
    }
}
