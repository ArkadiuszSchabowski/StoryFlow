using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Interfaces.Repositories;
using StoryFlow_Database.Entities;

namespace StoryFlow.Aggregates
{
    public class AggregateUserRepository : IAggregateUserRepository
    {
        private readonly IRepository<User> _repository;
        private readonly IGetAllRepository<User> _getAllRepository;
        private readonly IGetByEmailRepository _getByEmailRepository;
        private readonly IUpdateStars _updateStars;

        public AggregateUserRepository(IRepository<User> repository, IGetAllRepository<User> getAllRepository, IGetByEmailRepository getByEmailRepository, IUpdateStars updateStars)
        {
            _repository = repository;
            _getAllRepository = getAllRepository;
            _getByEmailRepository = getByEmailRepository;
            _updateStars = updateStars;
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

        public Task<User?> GetByEmail(string email)
        {
            return _getByEmailRepository.GetByEmail(email);
        }

        public async Task Remove(User entity)
        {
            await _repository.Remove(entity);
        }

        public async Task Update(User entity)
        {
            await _repository.Update(entity);
        }

        public async Task UpdateStars(User item)
        {
            await _updateStars.UpdateStars(item);
        }
    }
}
