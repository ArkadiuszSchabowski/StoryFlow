using StoryFlow.Interfaces;
using StoryFlow_Shared.Enums;

namespace StoryFlow.Helpers
{
    public class PointsCalculator : IPointsCalculator
    {
        public int GetBonusPointsForStoryLength(StorySize? size)
        {
            switch (size)
            {
                case StorySize.Short:
                    return 10;
                case StorySize.Medium:
                    return 20;
                case StorySize.Long:
                    return 30;
                default:
                    return 0;
            }
        }

        public int GetPointsPerAnswer(LanguageLevel? level)
        {
            switch (level)
            {
                case LanguageLevel.A1:
                    return 10;
                case LanguageLevel.A2:
                    return 15;
                case LanguageLevel.B1:
                    return 20;
                case LanguageLevel.B2:
                    return 25;
                case LanguageLevel.C1:
                    return 30;
                case LanguageLevel.C2:
                    return 35;
                default:
                    return 0;
            }
        }

        public int SetMaxPoints(StorySize? size, LanguageLevel? level)
        {
            int storySizePoints = 0;
            int pointsPerCorrectAnswer = 0;
            int questions = 4;

            switch (size)
            {
                case StorySize.Short:
                    storySizePoints = 10;
                    break;
                case StorySize.Medium:
                    storySizePoints = 20;
                    break;
                case StorySize.Long:
                    storySizePoints = 30;
                    break;
            }

            switch (level)
            {
                case LanguageLevel.A1:
                    pointsPerCorrectAnswer = 10;
                    break;
                case LanguageLevel.A2:
                    pointsPerCorrectAnswer = 15;
                    break;
                case LanguageLevel.B1:
                    pointsPerCorrectAnswer = 20;
                    break;
                case LanguageLevel.B2:
                    pointsPerCorrectAnswer = 25;
                    break;
                case LanguageLevel.C1:
                    pointsPerCorrectAnswer = 30;
                    break;
                case LanguageLevel.C2:
                    pointsPerCorrectAnswer = 35;
                    break;
            }

            return pointsPerCorrectAnswer * questions + storySizePoints;
        }
    }
}
