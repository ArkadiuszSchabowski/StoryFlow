using AutoMapper;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddStoryDto, Story>();
            CreateMap<AddSentenceDto, Sentence>();
            CreateMap<AddUserStoryDto, UserStory>();
            CreateMap<AddUserSentenceDto, UserSentence>();
        }
    }
}
