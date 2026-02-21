using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;

namespace StoryFlow.Aggregates
{
    public class AggregateUserRepository : IAggregateUserRepository
    {
        private readonly IRepository<User> _repository;
        private readonly IGetAllRepository<User> _getAllRepository;

        public AggregateUserRepository(IRepository<User> repository, IGetAllRepository<User> getAllRepository)
        {
            _repository = repository;
            _getAllRepository = getAllRepository;
        }
        public async Task Add(User entity)
        {
         await _repository.Add(entity);   
        }

        public async Task<User?> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<ICollection<User>> Get()
        {
            return await _getAllRepository.Get();
        }

        public async Task Remove(User entity)
        {
            await _repository.Remove(entity);
        }

        public async Task Update()
        {
            await _repository.Update();
        }
    }
}
