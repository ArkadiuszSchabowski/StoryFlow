namespace StoryFlow.Interfaces
{
    public interface IGetAllRepository<T> where T : class
    {
        Task<ICollection<T>> Get();
    }
}
