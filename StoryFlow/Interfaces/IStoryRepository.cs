using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow_Shared.Interfaces
{
    public interface IStoryRepository
    {
        Task Add(Story entity);
        Task<Story?> Get(int id);
        Task<ICollection<Story>> Get(StoryFilter filter);
        Task Remove(Story entity);
        Task Update();
    }
}
