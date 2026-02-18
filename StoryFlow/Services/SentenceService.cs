using StoryFlow.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Services
{
    public class SentenceService : ISentenceService
    {
        public Task Add(AddSentenceDto item)
        {
            throw new NotImplementedException();
        }

        public Task<GetSentenceDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<GetSentenceDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
