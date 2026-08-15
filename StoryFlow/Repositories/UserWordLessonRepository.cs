using Microsoft.EntityFrameworkCore;
using StoryFlow.Interfaces;
using StoryFlow_Database;
using StoryFlow_Database.Entities;

namespace StoryFlow.Repositories
{
    public class UserWordLessonRepository : IUserWordLessonRepository
    {
        private readonly MyDbContext _context;

        public UserWordLessonRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task Add(UserWordLesson userWordLesson)
        {
            await _context.UserWordLessons.AddAsync(userWordLesson);
            _context.SaveChanges();
        }

        public async Task<UserWordLesson?> GetByUserAndWordLesson(int userId, int? wordLessonId)
        {
            return await _context.UserWordLessons
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WordLessonId == wordLessonId);
        }

        public async Task Update(UserWordLesson userWordLesson)
        {
            _context.UserWordLessons.Update(userWordLesson);
            await _context.SaveChangesAsync();
        }
    }
}
