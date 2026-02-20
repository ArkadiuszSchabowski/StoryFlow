using Microsoft.AspNetCore.Mvc;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IGetFilteredStories
    {
        Task<List<GetStoryDto>> Get(LanguageLevel? languageLevel, StoryCategory? category, StorySize? size);
    }
}
