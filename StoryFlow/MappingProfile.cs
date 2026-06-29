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
            CreateMap<AddQuizDto, Quiz>();
            CreateMap<AddQuestionDto, Question>();
            CreateMap<AddAnswerDto, Answer>();
            CreateMap<Story, GetStoryDto>();
            CreateMap<StoryPoint, GetStoryPointDto>();
            CreateMap<Quiz, GetQuizDto>();
            CreateMap<Question, GetQuestionDto>();
            CreateMap<Answer, GetAnswerDto>();
            CreateMap<Sentence, GetSentenceDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Hobby, GetHobbyDto>();
            CreateMap<UserStory, GetUserStoryDto>();
            CreateMap<UserSentence, GetUserSentenceDto>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
