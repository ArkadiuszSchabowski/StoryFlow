using StoryFlow_Shared.Enums;

namespace StoryFlow.Interfaces
{
    public interface IPointsCalculator
    {
        int SetMaxPoints(StorySize? size, LanguageLevel? level);
    }
}
