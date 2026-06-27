using StoryFlow_Shared.Enums;

namespace StoryFlow.Interfaces.Validators
{
    public interface IResultValidator
    {
        void ValidateResult(StorySize? size, LanguageLevel? languageLevel, int result);
    }
}
