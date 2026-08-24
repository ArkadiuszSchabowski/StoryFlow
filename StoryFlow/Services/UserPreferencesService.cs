using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;

namespace StoryFlow.Services
{
    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly IRepository<User> _userRepository;

        public UserPreferencesService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task SetTheme(int userId, bool theme)
        {
            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            if(user.UserPreferences == null)
            {
                user.UserPreferences = new UserPreferences();
                user.UserPreferences.isLightTheme = theme;
            }

            else
            {
            user.UserPreferences.isLightTheme = theme;
            }

            await _userRepository.Update(user);
        }
    }
}
