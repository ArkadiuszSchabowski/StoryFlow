using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IGet<T> where T : class
    {
        public Task<ICollection<T>> Get();
        Task<T?> Get(int id);
    }
}
