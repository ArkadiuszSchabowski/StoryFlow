using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class UserService : IUserService
    {
        private readonly IAggregateUserValidator _userValidator;
        private readonly IAggregateUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;


        public UserService(IAggregateUserValidator userValidator, IAggregateUserRepository userRepository, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _userValidator = userValidator;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task Add(AddUserDto dto)
        {
            _userValidator.ValidateDto(dto);

            User? existedUser = await _userRepository.GetByEmail(dto.Email);

            if (existedUser != null) {
                throw new ConflictException("This email is already registered.");
            }

            var user = _mapper.Map<User>(dto);

            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);

            user.HashedPassword = hashedPassword;

            await _userRepository.Add(user);
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
