using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IAggregateWordRepository
    {
        Task<WordLesson?> Get(int id);
        IQueryable<Story> GetBySeason(int userId, int? seasonId);
        Task SaveBestResult(UserWordLesson userWordLesson);
    }
}
