using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IUserWordLessonRepository
    {
        Task<UserWordLesson?> GetByUserAndWordLesson(int userId, int? wordLessonId);
        Task Add(UserWordLesson userWordLesson);
        Task Update(UserWordLesson userWordLesson);
    }
}
