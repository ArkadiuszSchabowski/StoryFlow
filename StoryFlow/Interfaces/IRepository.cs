namespace StoryFlow_Shared.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item);
        T Get(int id);
        ICollection<T> GetAll();
        void Remove();
        void Update();
    }
}
