using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IGetByEmailRepository
    {
        Task<User?> GetByEmail(string email);
    }
}
