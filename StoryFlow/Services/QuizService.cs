using AutoMapper;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class QuizService : IQuizService
    {
        private readonly IAggregateStoryRepository _storyRepository;
        private readonly IEntityValidator<Story> _validator;
        private readonly IMapper _mapper;

        public QuizService(IAggregateStoryRepository storyRepository, IEntityValidator<Story> validator, IMapper mapper)
        {
            _storyRepository = storyRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task Add(AddQuizDto dto)
        {
            Story? result = await _storyRepository.Get(dto.StoryId);

            _validator.ThrowIsNull(result);

            if(result!.Quiz != null)
            {
                throw new BadRequestException($"Story '{result.EnglishTitle}' (Id: {result.Id}) already has a quiz.");
            }

            Quiz quiz = _mapper.Map<Quiz>(dto);

            result!.Quiz = quiz;

            await _storyRepository.SaveChangesAsync();
        }
    }
}
