using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StoryFlow.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IAggregateServiceValidator _serviceValidator;
        private readonly ISentenceBuilder _sentenceBuilder;
        private readonly ITextConverter _textConverter;
        private readonly ITextCounter _textCounter;
        private readonly IMapper _mapper;

        public StoryService(IStoryRepository storyRepository, IAggregateServiceValidator serviceValidator, ISentenceBuilder sentenceBuilder, ITextConverter textConverter, ITextCounter textCounter, IMapper mapper)
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

        public async Task<ICollection<GetStoryDto>> Get(StoryFilter? filter)
        {
            var query = _storyRepository.Get();

            if (filter != null)
            {
                if (filter.LanguageLevel.HasValue)
                    query = query.Where(x => x.LanguageLevel == filter.LanguageLevel.Value);

                if (filter.Category.HasValue)
                    query = query.Where(x => x.StoryCategory == filter.Category.Value);

                if (filter.Size.HasValue)
                    query = query.Where(x => x.StorySize == filter.Size.Value);
            }

            var results = await query.ToListAsync();

            var stories = _mapper.Map<List<GetStoryDto>>(results);

            return stories;
        }

        public async Task<GetStoryDto> Get(int id)
        {
            _serviceValidator.ValidateId(id);

            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            var storyDto = _mapper.Map<GetStoryDto>(result);

            return storyDto;
        }

        public async Task Remove(int id)
        {
            _serviceValidator.ValidateId(id);

            var result = await _storyRepository.Get(id);

            _serviceValidator.ThrowIsNull(result);

            await _storyRepository.Remove(result!);
        }
    }
}
