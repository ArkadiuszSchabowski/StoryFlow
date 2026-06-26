using StoryFlow_Shared.Enums;

namespace StoryFlow.Interfaces
{
    public interface IPointsCalculator
    {
        int SetMaxPoints(StorySize? size, LanguageLevel? level);
        int GetPointsPerAnswer(LanguageLevel? level);
        int GetBonusPointsForStoryLength(StorySize? size);
    }
}
