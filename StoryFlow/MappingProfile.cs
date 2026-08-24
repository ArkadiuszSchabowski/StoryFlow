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
            CreateMap<AddBlogPostDto, BlogPost>();
            CreateMap<AddBlogPostSectionDto, BlogPostSection>();
            CreateMap<AddUserWordLessonDto, WordLesson>();
            CreateMap<Story, GetStoryDto>();
            CreateMap<StoryPoint, GetStoryPointDto>();
            CreateMap<WordPoint, GetWordPointDto>();
            CreateMap<Quiz, GetQuizDto>();
            CreateMap<Word, GetWordDto>();
            CreateMap<Question, GetQuestionDto>();
            CreateMap<Answer, GetAnswerDto>();
            CreateMap<Sentence, GetSentenceDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<WordLesson, GetWordLessonDto>();
            CreateMap<Hobby, GetHobbyDto>();
            CreateMap<UserPreferences, GetUserPreferencesDto>();
            CreateMap<GetUserPreferencesDto, UserPreferences>();
            CreateMap<UserStory, GetUserStoryDto>();
            CreateMap<UserWordLesson, GetUserWordLessonDto>();
            CreateMap<UserSentence, GetUserSentenceDto>();
            CreateMap<BlogPost, GetBlogPostDto>();
            CreateMap<BlogPostSection, GetBlogPostSectionDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<StorySeason, GetStorySeasonDto>();
            CreateMap<GetUserDto, User>();
        }
    }
}
