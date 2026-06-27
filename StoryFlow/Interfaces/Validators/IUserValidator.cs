using StoryFlow_Shared.Models;

namespace StoryFlow.Interfaces.Validators
{
    public interface IUserValidator
    {
        void ValidateDto(RegisterUserDto dto);
    }
}
