using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;

namespace StoryFlow.Aggregates
{
    public class AggregateStoryRepository : IAggregateStoryRepository
    {
        private readonly IRepository<Story> _repository;
        private readonly IGetStoryRepository _storyRepository;
        private readonly IUserStoryRepository _userStoryRepository;

        public AggregateStoryRepository(IRepository<Story> repository, IGetStoryRepository storyRepository, IUserStoryRepository userStoryRepository)
        {
            _repository = repository;
            _storyRepository = storyRepository;
            _userStoryRepository = userStoryRepository;
        }
        public async Task Add(Story entity)
        {
            await _repository.Add(entity);
        }

        public async Task AddBestResult(UserStory userStory)
        {
            await _userStoryRepository.AddBestResult(userStory);
        }

        public async Task<Story?> Get(int id)
        {
            return await _repository.Get(id);
        }

        public IQueryable<Story> Get()
        {
            return _storyRepository.Get();
        }

        public Task<UserStory?> GetByUserAndStory(int userId, int storyId)
        {
            return _userStoryRepository.GetByUserAndStory(userId, storyId);
        }

        public async Task Remove(Story entity)
        {
            await _repository.Remove(entity);
        }

        public async Task Update(UserStory userStory)
        {
            await _userStoryRepository.Update(userStory);
        }

        public async Task SaveChangesAsync()
        {
            await _userStoryRepository.SaveChangesAsync();
        }
    }
}
