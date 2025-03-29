using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAO
{
    public class ChapterDAO : IChapterDao
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

        public List<Chapter> GetAllChapterByBookId(int bookId)
        {
            return _context.Chapters.Where(x => x.BookId == bookId).ToList();
        }

        public void AddChapter(Chapter chapter)
        {
            _context.Chapters.Add(chapter);
            _context.SaveChanges();
        }

        public void UpdateChapter(Chapter chapter)
        {
            var exist = _context.Chapters.Find(chapter.ChapterId);
            if (exist != null)
            {
                exist.ChapterName = chapter.ChapterName;
                exist.Contents1 = chapter.Contents1;
                exist.Contents2 = chapter.Contents2;
                _context.Chapters.Update(exist);
                _context.SaveChanges();
            }

        }

        public void DeleteChapter(int id)
        {
            var exist = _context.Chapters.Find(id);
            var reading = _context.Readings.Where(x => x.Chapterid == id);
            if (reading.Any())
            {
                _context.Readings.RemoveRange(reading);
            }
            if (exist != null)
            {
                _context.Chapters.Remove(exist);
                
            }
            _context.SaveChanges();
        }
    }
}