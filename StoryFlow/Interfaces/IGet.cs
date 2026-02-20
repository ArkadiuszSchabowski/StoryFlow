namespace StoryFlow_Shared.Interfaces
{
    public interface IGet<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(int id);
    }
}
