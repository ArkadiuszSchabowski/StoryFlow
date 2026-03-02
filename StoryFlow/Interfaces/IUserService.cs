using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IUserService : IAdd<RegisterUserDto>, IGet<GetUserDto>, IRemove
    {
        Task<TokenDto> Login(LoginDto dto);
    }
}
