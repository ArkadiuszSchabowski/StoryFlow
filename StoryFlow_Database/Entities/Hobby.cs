namespace StoryFlow_Database.Entities
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserHobby> UserHobbies { get; set; } = new List<UserHobby>();
    }
}
