using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class ChapterDAO
    {
        private readonly PRN221_Project_1Context _context;

        public ChapterDAO(PRN221_Project_1Context context)
        {
            _context = context;
        }

        public async Task<List<Chapter>> GetChaptersByBookIdAsync(int bookId)
        {
            return await _context.Chapters
                .Where(x => x.BookId == bookId)
                .ToListAsync();
        }

        public async Task<Chapter> GetChapterByIdAsync(int id)
        {
            return await _context.Chapters.FindAsync(id);
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<Reading> GetReadingAsync(int userId, int chapterId)
        {
            return await _context.Readings
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Chapterid == chapterId);
        }

        public async Task AddReadingAsync(Reading reading)
        {
            _context.Readings.Add(reading);
            await _context.SaveChangesAsync();
        }

        public async Task<Chapter> GetChapterByNumberAsync(int bookId, int numberChapter)
        {
            return await _context.Chapters
                .FirstOrDefaultAsync(x => x.BookId == bookId && x.NumberChapter == numberChapter);
        }

        public async Task AddChapterAsync(Chapter chapter)
        {
            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChapterAsync(Chapter chapter)
        {
            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterAsync(Chapter chapter)
        {
            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();
        }
    }
}