using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IQuizService : IAdd<AddQuizDto>
    {
        Task<QuestionSubmissionResult> CheckAnswer(QuestionSubmissionDto dto, string userIdString);
        Task<QuizResult> CheckAnswers(QuizSubmissionDto dto, string userIdString);
    }
}
