using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAO
{
    public class ChapterDAO: IChapterDao
    {
        private readonly PRN221_Project_1Context _context;

        public ChapterDAO(PRN221_Project_1Context context)
        {
            _context = context;  

        }
        
        public Book GetBookById(int bookId)
        {
            return _context.Books.FirstOrDefault(x => x.BookId == bookId);
        }

        public Chapter GetChapterById(int id)
        {
            return _context.Chapters.Find(id);
        }
        public void AddReading(Reading reading)
        {
            _context.Readings.Add(reading);
            _context.SaveChanges();
        }

        public Reading GetReading(int userId, int chapterId)
        {
            return _context.Readings.FirstOrDefault(x => x.UserId == userId && x.Chapterid == chapterId);
        }
    }
}