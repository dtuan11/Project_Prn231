using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API.DAO
{
    public class BookDAO: IBookDao
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
        public Book CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }
     
        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        public void DeleteRelatedData(int bookId)
        {
            var categoriesInBooks = _context.CategoryInBooks.Where(c => c.BookId == bookId);
            var rates = _context.Rates.Where(r => r.BookId == bookId);
            var readings = _context.Readings.Where(r => r.Bookid == bookId);
            var saveBooks = _context.SavedBooks.Where(s => s.BookId == bookId);
            var comments = _context.Comments.Where(c => c.BookId == bookId);
            var chapters = _context.Chapters.Where(c => c.BookId == bookId);
            var reports = _context.Reports.Where(r => r.BookId == bookId);

            // Lấy toàn bộ các reportId liên quan đến book cần xóa
            var reportIds = reports.Select(r => (int?)r.ReportId).ToList();

            var responses = _context.Responses
                                    .Where(resp => reportIds.Contains(resp.ReportId))
                                    .ToList();

            _context.Responses.RemoveRange(responses);

            // Sau đó mới xóa các Report
            _context.Reports.RemoveRange(reports);

            // Xóa các bảng liên quan khác
            _context.CategoryInBooks.RemoveRange(categoriesInBooks);
            _context.Rates.RemoveRange(rates);
            _context.Readings.RemoveRange(readings);
            _context.SavedBooks.RemoveRange(saveBooks);
            _context.Comments.RemoveRange(comments);
            _context.Chapters.RemoveRange(chapters);

            _context.SaveChanges();
        }



    }
}