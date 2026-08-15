using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface ISeasonRepository
    {
        StorySeason? GetBySeason(int userId, int? seasonId);
    }
}
