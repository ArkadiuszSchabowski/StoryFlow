using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly MyDbContext _context;

        public AnswerRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task Add(Answer entity)
        {
            await _context.Answers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Answer?> Get(int id)
        {
            return await _context.Answers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Remove(Answer entity)
        {
            _context.Answers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(Answer entity)
        {
            throw new NotImplementedException();
        }
    }
}
