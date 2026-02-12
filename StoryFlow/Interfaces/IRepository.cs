namespace StoryFlow_Shared.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item);
        Task<T> Get(int id);
        Task<ICollection<T>> GetAll();
        void Remove();
        void Update();
    }
}
