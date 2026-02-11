using AutoMapper;
using StoryFlow.Exceptions;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class StoryService : IService
    {
        private readonly IRepository<Story> _storyRepository;
        private readonly IValidator<AddStoryDto> _storyValidator;
        private readonly ITextConverter _textConverter;
        private readonly ITextCounter _textCounter;
        private readonly IMapper _mapper;

        public StoryService(IRepository<Story> storyRepository, IValidator<AddStoryDto> storyValidator, ITextConverter textConverter, ITextCounter textCounter, IMapper mapper)
        {
            _storyRepository = storyRepository;
            _storyValidator = storyValidator;
            _textConverter = textConverter;
            _textCounter = textCounter;
            _mapper = mapper;
        }
        public async Task Add(AddStoryDto item)
        {
            _storyValidator.Validate(item);

            var story = _mapper.Map<Story>(item);

            story.StorySize = _textCounter.SetTextSize(item.EnglishStory!);

            var englishSentences = _textConverter.GetSentencesFromText(item.EnglishStory!);
            var polishSentences = _textConverter.GetSentencesFromText(item.PolishStory!);

            var englishSentencesLength = englishSentences.Count();
            var polishSentencesLength = polishSentences.Count();

            if(polishSentencesLength != englishSentencesLength)
            {
                throw new BadRequestException("Polish sentences are not equal to english sentences");
            }

            for(var i = 0; i < englishSentencesLength; i++)
            {
                var sentence = new Sentence()
                {
                    PolishMeaning = polishSentences[i],
                    EnglishMeaning = englishSentences[i],
                    Order = i
                };

                story.Sentences.Add(sentence);
            }
            await _storyRepository.Add(story);
        }

        public void Get(ICollection<GetStoryDto> collection)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
