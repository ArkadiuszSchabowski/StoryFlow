using StoryFlow_Shared.Enums;

namespace StoryFlow_Shared.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item);
        Task<T?> Get(int id);
        Task<ICollection<T>> GetAll();
        Task<ICollection<T>> Get(LanguageLevel? languageLevel, StoryCategory? category, StorySize? size); //refactor
        void Remove(T entity);
        void Update();
    }
}
