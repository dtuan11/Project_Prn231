using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDAO _bookDAO;

        public BookRepository(BookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookDAO.GetAllBooksAsync();
        }

        public async Task<List<Book>> GetBooksByUserAsync(int userId, int roleId)
        {
            return await _bookDAO.GetBooksByUserAsync(userId, roleId);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookDAO.GetBookByIdAsync(id);
        }

        public async Task<(double AverageRating, int RateCount)> GetBookRatingsAsync(int bookId)
        {
            var ratings = await _bookDAO.GetBookRatingsAsync(bookId);
            var rateCount = ratings.Count;
            var averageRating = rateCount > 0 ? ratings.Average() : 0;
            return (averageRating, rateCount);
        }

        public async Task UpdateBookAsync(Book book, List<int> categoryIds)
        {
            book.Status = "Updating";
            var existingCategories = await _bookDAO.GetCategoriesInBookAsync(book.BookId);

            foreach (var categoryId in categoryIds)
            {
                if (!existingCategories.Contains(categoryId))
                {
                    await _bookDAO.AddCategoryInBookAsync(new CategoryInBook
                    {
                        CateId = categoryId,
                        BookId = book.BookId
                    });
                }
            }

            await _bookDAO.UpdateBookAsync(book);
        }

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            return await _bookDAO.SearchBooksAsync(keyword);
        }

        public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _bookDAO.GetBooksByCategoryAsync(categoryId);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _bookDAO.CategoryExistsAsync(categoryId);
        }
    }
}