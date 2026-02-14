using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Helpers
{
    public class TextConverter : ITextConverter
    {
        public List<string> GetSentencesFromText(string text)
        {
            var arraySentence = text.Split('.');
            var listSentence = new List<string>();

            for(int i=0; i<arraySentence.Length - 1; i++)
            {
                var newItem = arraySentence[i].Trim() + '.';
                listSentence.Add(newItem);
            }

            return listSentence;
        }
    }
}
