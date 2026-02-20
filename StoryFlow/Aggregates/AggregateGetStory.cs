using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Aggregates
{
    public class AggregateGetStory : IAggregateGetStory
    {
        private readonly IGet<GetStoryDto> _getStory;
        private readonly IGetFilteredStories _getFilteredStories;

        public AggregateGetStory(IGet<GetStoryDto> getStory, IGetFilteredStories getFilteredStories)
        {
            _getStory = getStory;
            _getFilteredStories = getFilteredStories;
        }
        public Task<GetStoryDto> Get(int id)
        {
            return _getStory.Get(id);
        }

        public Task<List<GetStoryDto>> Get(LanguageLevel? languageLevel, StoryCategory? category, StorySize? size)
        {
            return _getFilteredStories.Get(languageLevel, category, size);
        }

        public Task<ICollection<GetStoryDto>> GetAll()
        {
            return _getStory.GetAll();
        }
    }
}
