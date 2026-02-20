using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces.Aggregates
{
    public interface IAggregateGetStory : IGet<GetStoryDto>, IGetFilteredStories
    {

    }
}
