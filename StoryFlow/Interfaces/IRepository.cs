using StoryFlow_Database.Entities;

namespace StoryFlow_Shared.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task<T?> Get(int id);
        Task Remove(T entity);
    }
}
