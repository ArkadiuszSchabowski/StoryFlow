using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Services;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Enums;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow_Tests.UnitTests.Services
{
    public class StoryServiceUnitTests
    {
        private readonly Mock<IStoryRepository> _mockRepository;
        private readonly Mock<IAggregateServiceValidator> _mocServiceValidator;
        private readonly Mock<ISentenceBuilder> _mockSentenceBuilder;
        private readonly Mock<ITextConverter> _mockTextConverter;
        private readonly Mock<ITextCounter> _mockTextCounter;
        private readonly Mock<IMapper> _mockMapper;

        private readonly StoryService _storyService;

        private string _englishStory = "Beyond the mountains and forests lived a little squirrel named Tosia. Every day she gathered nuts, dreaming of a big pantry for the winter. One day she discovered a mysterious hollow full of the tastiest nuts in the entire forest.";

        private string _polishStory = "Za górami i lasami mieszkała mała wiewiórka o imieniu Tosia. Codziennie zbierała orzechy, marząc o wielkiej spiżarni na zimę. Pewnego dnia odkryła tajemniczą dziuplę pełną najsmaczniejszych orzechów w całym lesie.";

        private List<string> _listEnglishSentences = new List<string>(){
                "Beyond the mountains and forests lived a little squirrel named Tosia.",
                "Every day she gathered nuts, dreaming of a big pantry for the winter.",
                "One day she discovered a mysterious hollow full of the tastiest nuts in the entire forest."
            };

        private List<string> _listPolishSentences = new List<string>()
            {
                "Za górami i lasami mieszkała mała wiewiórka o imieniu Tosia.",
                "Codziennie zbierała orzechy, marząc o wielkiej spiżarni na zimę.",
                "Pewnego dnia odkryła tajemniczą dziuplę pełną najsmaczniejszych orzechów w całym lesie."
            };

        public StoryServiceUnitTests()
        {
            _mockRepository = new Mock<IStoryRepository>();
            _mocServiceValidator = new Mock<IAggregateServiceValidator>();
            _mockSentenceBuilder = new Mock<ISentenceBuilder>();
            _mockTextConverter = new Mock<ITextConverter>();
            _mockTextCounter = new Mock<ITextCounter>();
            _mockMapper = new Mock<IMapper>();

            _storyService = new StoryService( _mockRepository.Object,  _mocServiceValidator.Object, _mockSentenceBuilder.Object,_mockTextConverter.Object, _mockTextCounter.Object,_mockMapper.Object);
        }
        [Fact]
        public async Task Add_WithCorrectModel_InvokesStoryReposiryAdd()
        {
            var storyDto = new AddStoryDto
            {
                PolishTitle = "Zwierzęca historia",
                EnglishTitle = "Animal Story",
                StoryCategory = StoryCategory.Animals,
                LanguageLevel = LanguageLevel.A1,
                PolishStory = _polishStory,
                EnglishStory = _englishStory
            };

            var story = new Story
            {
                Id = 1,
                PolishTitle = "Zwierzęca historia",
                EnglishTitle = "Animal Story",
                StoryCategory = StoryCategory.Animals,
                LanguageLevel = LanguageLevel.A1
            };

            _mockMapper.Setup(x => x.Map<Story>(storyDto)).Returns(story);

            _mockTextConverter.Setup(x => x.GetSentencesFromText(_englishStory)).Returns(_listEnglishSentences);
            _mockTextConverter.Setup(x => x.GetSentencesFromText(_polishStory)).Returns(_listPolishSentences);

            await _storyService.Add(storyDto);

            _mockRepository.Verify(x => x.Add(story), Times.Once);
        }

        [Fact]
        public async Task Get_WithCorrectId_ReturnsTypeOfGetStoryDto()
        {
            int id = 1;

            var story = new Story
            {
                Id = 1,
                PolishTitle = "Zwierzęca historia",
                EnglishTitle = "Animal Story",
            };

            var getStoryDto = new GetStoryDto
            {
                Id = 1,
                PolishTitle = "Zwierzęca historia",
                EnglishTitle = "Animal Story",
            };

            _mockRepository.Setup(x => x.Get(1)).ReturnsAsync(story);
            _mockMapper.Setup(x => x.Map<GetStoryDto>(story)).Returns(getStoryDto);

            var result = await _storyService.Get(id);

            result.Should().BeOfType<GetStoryDto>();
        }

        [Fact]
        public async Task Remove_WhenStoryExists_ShouldCallRepositoryRemoveOnce()
        {
            int id = 1;

            var story = new Story
            {
                Id = 1,
                PolishTitle = "Zwierzęca historia",
                EnglishTitle = "Animal Story",
            };

            _mockRepository.Setup(x => x.Get(1)).ReturnsAsync(story);

            await _storyService.Remove(id);

            _mockRepository.Verify(x => x.Remove(story), Times.Once);
        }
    }
}
