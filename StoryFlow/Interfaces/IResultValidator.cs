using StoryFlow_Shared.Enums;

namespace StoryFlow.Interfaces
{
    public interface IResultValidator
    {
        void ValidateResult(StorySize? size, LanguageLevel? languageLevel, int result);
    }
}
