using StoryFlow.Interfaces;
using StoryFlow_Database.Entities;

namespace StoryFlow.Builders
{
    public class SentenceBuilder :ISentenceBuilder
    {
        public void AddSentencesToStory(Story story, List<string> polishSentences, List<string> englishSentences)
        {
            for (var i = 0; i < englishSentences.Count; i++)
            {
                story.Sentences.Add(new Sentence
                {
                    PolishMeaning = polishSentences[i],
                    EnglishMeaning = englishSentences[i],
                    Order = i
                });
            }
        }
    }
}
