using StoryFlow_Database.Entities;
using StoryFlow_Shared.Models;

namespace StoryFlow_Shared.Interfaces
{
    public interface IService : IAdd<AddStoryDto>, IGet<GetStoryDto>, IRemove
    {

    }
}
