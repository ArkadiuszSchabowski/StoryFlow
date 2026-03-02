using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoryFlow.Services
{
    public class UserService : IUserService
    {
        private readonly IAggregateUserValidator _userValidator;
        private readonly IAggregateUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(IAggregateUserValidator userValidator, IAggregateUserRepository userRepository, IPasswordHasher<User> passwordHasher, IMapper mapper, AuthenticationSettings authenticationSettings)
        {
            _userValidator = userValidator;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }
        public async Task Add(RegisterUserDto dto)
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

        public async Task<TokenDto> Login(LoginDto dto)
        {
            User? user = await _userRepository.GetByEmail(dto.Email);

            if (user == null) {
                throw new BadRequestException("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresDays = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims,
                expires: expiresDays,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            var newToken = new TokenDto()
            {
                Token = tokenString
            };

            return newToken;
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
