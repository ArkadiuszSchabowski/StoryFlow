namespace StoryFlow_Shared.Interfaces
{
    public interface IAdd<T> where T : class
    {
        Task Add(T item);
    }
}
