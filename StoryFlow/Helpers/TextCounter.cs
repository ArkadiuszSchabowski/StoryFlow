using StoryFlow.Validators;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Helpers
{
    public class TextCounter : ITextCounter
    {
        private readonly IValidator<AddStoryDto> _storyValidator;

        public TextCounter(IValidator<AddStoryDto> storyValidator)
        {
            _storyValidator = storyValidator;
        }
        public StorySize? SetTextSize(string text)
        {            
            var counter = text.Length;

            if(text.Length >= 150 && text.Length < 300)
            {
                return StorySize.Short;
            }

            if (text.Length >= 300 && text.Length < 600)
            {
                return StorySize.Medium;
            }

            if (text.Length >= 600 && text.Length <= 1000)
            {
                return StorySize.Long;
            }
            return null;
        }
    }
}
