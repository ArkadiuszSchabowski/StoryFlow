using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IAnswerChecker
    {
        QuizResult CheckAnswers(Story story, List<AnswerDto> dto);
    }
}
