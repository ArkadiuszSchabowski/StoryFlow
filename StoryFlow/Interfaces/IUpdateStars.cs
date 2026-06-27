using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IUpdateStars
    {
        Task UpdateStars(User item);
    }
}
