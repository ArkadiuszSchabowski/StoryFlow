using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly MyDbContext _context;

        public QuizRepository(MyDbContext context)
        {
            _context = context;     
        }
        public int GetCorrectAnswer(Question? question)
        {
            return _context.Answers
                .Where(a => a.QuestionId == question!.Id && a.IsCorrect)
                .Select(a => a.Id)
                .FirstOrDefault();
        }
    }
}
