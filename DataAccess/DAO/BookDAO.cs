using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookDAO
    {
        private readonly PRN221_Project_1Context _context;

        public BookDAO(PRN221_Project_1Context context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Where(x => !x.Status.Equals("Delete") && x.Approve.Equals("Approved"))
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByUserAsync(int userId, int roleId)
        {
            if (roleId == 0) // Admin
            {
                return await _context.Books.Include(x => x.User).ToListAsync();
            }
            else if (roleId == 1) // Regular user
            {
                return await _context.Books
                    .Where(x => x.UserId == userId && !x.Status.Equals("Delete"))
                    .Include(x => x.User)
                    .ToListAsync();
            }
            return new List<Book>();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(x => x.Chapters)
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.BookId == id);
        }

        public async Task<List<int>> GetBookRatingsAsync(int bookId)
        {
            return await _context.Rates
                .Where(x => x.BookId == bookId)
                .Select(x => x.Point)
                .ToListAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddCategoryInBookAsync(CategoryInBook categoryInBook)
        {
            _context.CategoryInBooks.Add(categoryInBook);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetCategoriesInBookAsync(int bookId)
        {
            return await _context.CategoryInBooks
                .Where(x => x.BookId == bookId)
                .Select(x => x.CateId)
                .ToListAsync();
        }

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            return await _context.Books
                .Where(x => x.Title.Contains(keyword) && x.Approve.Equals("Approved") && !x.Status.Equals("Delete"))
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.CategoryInBooks
                .Include(x => x.Book)
                .Where(x => x.CateId == categoryId && x.Book.Approve.Equals("Approved") && !x.Book.Status.Equals("Delete"))
                .Select(x => x.Book)
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.CateId == categoryId);
        }
    }
}