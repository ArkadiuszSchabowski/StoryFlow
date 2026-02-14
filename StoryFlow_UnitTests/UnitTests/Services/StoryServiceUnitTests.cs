using AutoMapper;
using FluentAssertions;
using Moq;
using StoryFlow.Interfaces;
using StoryFlow.Services;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow_Tests.UnitTests.Services
{
    public class StoryServiceUnitTests
    {
        private readonly Mock<IRepository<Story>> _mockRepository;
        private readonly Mock<IAggregateServiceValidator> _mocServiceValidator;
        private readonly Mock<ITextConverter> _mockTextConverter;
        private readonly Mock<ITextCounter> _mockTextCounter;
        private readonly Mock<IMapper> _mockMapper;

        public StoryServiceUnitTests()
        {
            _mockRepository = new Mock<IRepository<Story>>();
            _mocServiceValidator = new Mock<IAggregateServiceValidator>();
            _mockTextConverter = new Mock<ITextConverter>();
            _mockTextCounter = new Mock<ITextCounter>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_WithoutResults_ReturnsEmptyList()
        {
            var storyService = new StoryService(_mockRepository.Object, _mocServiceValidator.Object, _mockTextConverter.Object, _mockTextCounter.Object, _mockMapper.Object);

            var results = new List<Story>();
            var resultsDto = new List<GetStoryDto>();

            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(results);

            _mockMapper.Setup(x => x.Map<List<GetStoryDto>>(results)).Returns(resultsDto);

            var result = await storyService.GetAll();

            result.Should().BeEquivalentTo(resultsDto);
        }
        [Fact]
        public async Task Get_WithCorrectId_ReturnsTypeOfGetStoryDto()
        {
            var storyService = new StoryService(_mockRepository.Object, _mocServiceValidator.Object, _mockTextConverter.Object, _mockTextCounter.Object, _mockMapper.Object);

            int id = 1;

            var story = new Story
            {
                Id = 1,
                Title = "First Story"
            };

            var getStoryDto = new GetStoryDto
            {
                Id = 1,
                Title = "First Story"
            };

            _mockRepository.Setup(x => x.Get(1)).ReturnsAsync(story);
            _mockMapper.Setup(x => x.Map<GetStoryDto>(story)).Returns(getStoryDto);

            var result = await storyService.Get(id);

            result.Should().BeOfType<GetStoryDto>();
        }

        [Fact]
        public async Task Remove_WhenStoryExists_ShouldCallRepositoryRemoveOnce()
        {
            var storyService = new StoryService(_mockRepository.Object, _mocServiceValidator.Object, _mockTextConverter.Object, _mockTextCounter.Object, _mockMapper.Object);

            int id = 1;

            var story = new Story
            {
                Id = 1,
                Title = "First Story"
            };

            _mockRepository.Setup(x => x.Get(1)).ReturnsAsync(story);

            await storyService.Remove(id);

            _mockRepository.Verify(x => x.Remove(story), Times.Once);
        }
    }
}
