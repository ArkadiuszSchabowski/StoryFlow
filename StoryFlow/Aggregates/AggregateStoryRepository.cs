using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Aggregates
{
    public class AggregateStoryRepository : IAggregateStoryRepository
    {
        private readonly IRepository<Story> _repository;
        private readonly IStoryRepository _storyRepository;

        public AggregateStoryRepository(IRepository<Story> repository, IStoryRepository storyRepository)
        {
            _repository = repository;
            _storyRepository = storyRepository;
        }
        public async Task Add(Story entity)
        {
            await _repository.Add(entity);
        }

        public async Task<Story?> Get(int id)
        {
            return await _repository.Get(id);
        }

        public IQueryable<Story> Get()
        {
            return _storyRepository.Get();
        }

        public async Task Remove(Story entity)
        {
            await _repository.Remove(entity);
        }

        public async Task Update()
        {
            await _repository.Update();
        }
    }
}
