using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IGetByEmailRepository
    {
        Task<User?> GetByEmail(string email);
    }
}
