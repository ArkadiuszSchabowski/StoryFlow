using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IQuizRepository
    {
        int GetCorrectAnswer(Question? question);
    }
}
