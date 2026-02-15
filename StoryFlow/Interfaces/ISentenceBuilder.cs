using StoryFlow_Database.Entities;

namespace StoryFlow.Interfaces
{
    public interface ISentenceBuilder
    {
        void AddSentencesToStory(Story story, List<string> polishSentences, List<string> englishSentences);
    }
}
