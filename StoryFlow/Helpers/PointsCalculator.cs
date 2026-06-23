using StoryFlow.Interfaces;
using StoryFlow_Shared.Enums;

namespace StoryFlow.Helpers
{
    public class PointsCalculator : IPointsCalculator
    {
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
