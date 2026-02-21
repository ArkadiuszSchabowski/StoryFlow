using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Helpers
{
    public class TextCounter : ITextCounter
    {
        public StorySize? SetTextSize(string text)
        {            
            if(text.Length >= 400 && text.Length < 700)
            {
                return StorySize.Short;
            }

            if (text.Length >= 700 && text.Length < 1000)
            {
                return StorySize.Medium;
            }

            if (text.Length >= 1000 && text.Length <= 1500)
            {
                return StorySize.Long;
            }
            return null;
        }
    }
}
