namespace StoryFlow.Interfaces
{
    public interface IUserPreferencesService
    {
        public Task SetTheme(int userId, bool theme);
    }
}
