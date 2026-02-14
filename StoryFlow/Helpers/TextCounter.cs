using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Helpers
{
    public class TextCounter : ITextCounter
    {
        public StorySize? SetTextSize(string text)
        {            
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
