using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces
{
    public interface IUserValidator
    {
        void ValidateDto(RegisterUserDto dto);
    }
}
