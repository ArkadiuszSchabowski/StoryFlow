using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MyDbContext _context;

        public QuestionRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task Add(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Question?> Get(int id)
        {
            return await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Remove(Question entity)
        {
            _context.Questions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
