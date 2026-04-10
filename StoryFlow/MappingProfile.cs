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
            CreateMap<Story, GetStoryDto>();
            CreateMap<Sentence, GetSentenceDto>();
            CreateMap<UserStory, GetUserStoryDto>();
            CreateMap<UserSentence, GetUserSentenceDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<Hobby, GetHobbyDto>();
        }
    }
}
