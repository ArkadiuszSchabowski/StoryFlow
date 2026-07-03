using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task<T?> Get(int id);
        Task Remove(T entity);
        Task Update(T entity);
    }
}
