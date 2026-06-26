using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface IAnswerChecker
    {
        int CheckAnswers(Story story, List<AnswerDto> dto);
    }
}
