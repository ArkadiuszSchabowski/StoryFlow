using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class UserService : IUserService
    {
        public Task Add(AddUserDto item)
        {
            throw new NotImplementedException();
        }

        public GetUserDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<GetUserDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        Task<GetUserDto?> IGet<GetUserDto>.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
