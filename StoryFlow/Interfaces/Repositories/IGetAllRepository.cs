namespace StoryFlow.Interfaces.Repositories
{
    public interface IGetAllRepository<T> where T : class
    {
        Task<ICollection<T>> Get();
    }
}
