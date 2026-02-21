using AutoMapper;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class UserService : IUserService
    {
        private readonly IAggregateUserValidator _userValidator;
        private readonly IAggregateUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(IAggregateUserValidator userValidator, IAggregateUserRepository userRepository, IMapper mapper)
        {
            _userValidator = userValidator;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public Task Add(AddUserDto item)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserDto?> Get(int id)
        {
            _userValidator.ValidateId(id);

            var entity = await _userRepository.Get(id);

            _userValidator.ThrowIsNull(entity);

            var dto = _mapper.Map<GetUserDto>(entity);

            return dto;
        }

        public async Task<ICollection<GetUserDto>> Get()
        {
            var results = await _userRepository.Get();

            var listDto = _mapper.Map<List<GetUserDto>>(results);

            return listDto;
        }

        public async Task Remove(int id)
        {
            _userValidator.ValidateId(id);

            var entity = await _userRepository.Get(id);

            _userValidator.ThrowIsNull(entity);

            await _userRepository.Remove(entity!);
        }
    }
}
