using AutoMapper;
using StoryFlow.Interfaces;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class StoryService : IService
    {
        private readonly IRepository<Story> _storyRepository;
        private readonly IAggregateServiceValidator _serviceValidator;
        private readonly ISentenceBuilder _sentenceBuilder;
        private readonly ITextConverter _textConverter;
        private readonly ITextCounter _textCounter;
        private readonly IMapper _mapper;

        public StoryService(IRepository<Story> storyRepository, IAggregateServiceValidator serviceValidator, ISentenceBuilder sentenceBuilder, ITextConverter textConverter, ITextCounter textCounter, IMapper mapper)
        {
            _storyRepository = storyRepository;
            _serviceValidator = serviceValidator;
            _sentenceBuilder = sentenceBuilder;
            _textConverter = textConverter;
            _textCounter = textCounter;
            _mapper = mapper;
        }
        public async Task Add(AddStoryDto item)
        {
            _serviceValidator.Validate(item);

            var story = _mapper.Map<Story>(item);

            story.StorySize = _textCounter.SetTextSize(item.EnglishStory!);

            var englishSentences = _textConverter.GetSentencesFromText(item.EnglishStory!);
            var polishSentences = _textConverter.GetSentencesFromText(item.PolishStory!);

            _serviceValidator.ValidateSentencesCount(polishSentences.Count, englishSentences.Count);

            _sentenceBuilder.AddSentencesToStory(story, polishSentences, englishSentences);

            await _storyRepository.Add(story);
        }

        public async Task<GetStoryDto> Get(int id)
        {
            _serviceValidator.ValidateId(id);
            
            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            var storyDto = _mapper.Map<GetStoryDto>(result);

            return storyDto;
        }

        public async Task<ICollection<GetStoryDto>> GetAll()
        {
            var results = await _storyRepository.GetAll();
            var stories = _mapper.Map<List<GetStoryDto>>(results);

            return stories;
        }

        public async Task Remove(int id)
        {
            _serviceValidator.ValidateId(id);

            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            _storyRepository.Remove(result!);
        }
    }
}
