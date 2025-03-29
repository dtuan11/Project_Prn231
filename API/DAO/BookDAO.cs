using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.DAO
{
    public class BookDAO : IBookDao
    {
        private readonly PRN221_Project_1Context _context;

        public BookDAO(PRN221_Project_1Context context)
        {
            _context = context;
        }

        public List<Book> GetApprovedBooks()
        {
            return _context.Books
                .Where(x => !x.Status.Equals("Delete") && x.Approve.Equals("Approved"))
                .ToList();
        }

        public List<Book> GetNewBooks()
        {
            return _context.Books
                .Include(x => x.Chapters)
                .Where(x => !x.Status.Equals("Delete") && x.Approve.Equals("Approved") && x.Chapters.Any())
                .OrderByDescending(x => x.PublishDate)
                .ToList();
        }

        public Dictionary<int, Chapter> GetLatestChapters(List<Book> books)
        {
            var latestChapters = new Dictionary<int, Chapter>();
            foreach (var book in books)
            {
                var latestChapter = book.Chapters.OrderByDescending(x => x.NumberChapter).First();
                latestChapters.Add(book.BookId, latestChapter);
            }
            return latestChapters;
        }
        public List<Book> GetBooksByCategory(int categoryId)
        {
            return _context.CategoryInBooks
                .Include(x => x.Book)
                .Where(x => x.CateId == categoryId && x.Book.Approve.Equals("Approved") && !x.Book.Status.Equals("Delete"))
                .Select(x => x.Book)
                .ToList();
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.CateId == categoryId);
        }
        public List<Book> SearchBooks(string keyword)
        {
            return _context.Books
                .Where(x => x.Title.Contains(keyword) && x.Approve.Equals("Approved") && !x.Status.Equals("Delete"))
                .ToList();
        }
        public Book GetBookById(int id)
        {
            return _context.Books
                .Include(x => x.Chapters)
                .Include(x=>x.Rates)
                .FirstOrDefault(x => x.BookId == id);
        }
        public List<Rate> GetRatesByBookId(int bookId)
        {
            return _context.Rates
                .Where(x => x.BookId == bookId)
                .ToList();
        }

        public void RateBook(int point, int bookId, int userId)
        {
            var existRate = _context.Rates.FirstOrDefault(r => r.UserId == userId && r.BookId == bookId);
            if (existRate != null)
            {
                existRate.Point = point;
                _context.Rates.Update(existRate);
                _context.SaveChanges();
            }
            else
            {
                var rate = new API.Models.Rate
                {
                    BookId = bookId,
                    UserId = userId,
                    Point = point,
                };
                _context.Rates.Add(rate);
                _context.SaveChanges();
            }
        }
    }
}