using AutoMapper;
using StoryFlow.Exceptions;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly IAggregateUserRepository _userRepository;
        private readonly IAggregateStoryValidator _serviceValidator;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IMapper _mapper;

        public SeasonService(IAggregateUserRepository userRepository, IAggregateStoryValidator serviceValidator, ISeasonRepository seasonRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _serviceValidator = serviceValidator;
            _seasonRepository = seasonRepository;
            _mapper = mapper;
        }
        public async Task<GetStorySeasonDto> GetBySeason(int seasonId, string? userIdClaim)
        {
            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedException("Użytkownik nie ma uprawnień do wykonania tej operacji.");
            }

            User? user = await _userRepository.Get(userId);

            if (user == null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }

            _serviceValidator.ValidateId(seasonId);

            if (seasonId == 2)
            {
                if (user.Stars < 750)
                {
                    throw new BadRequestException("Nie masz wystarczającej ilości gwiazdek, by przejść do tego sezonu.");
                }
            }

            if (seasonId == 3)
            {
                if (user.Stars < 1600)
                {
                    throw new BadRequestException("Nie masz wystarczającej ilości gwiazdek, by przejść do tego sezonu.");
                }
            }


            StorySeason? season = _seasonRepository.GetBySeason(userId, seasonId);

            GetStorySeasonDto dto = _mapper.Map<GetStorySeasonDto>(season);

            return dto;
        }

    }
}
