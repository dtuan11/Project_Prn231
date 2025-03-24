using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetBooksByUserAsync(int userId, int roleId);
        Task<Book> GetBookByIdAsync(int id);
        Task<(double AverageRating, int RateCount)> GetBookRatingsAsync(int bookId);
        Task UpdateBookAsync(Book book, List<int> categoryIds);
        Task<List<Book>> SearchBooksAsync(string keyword);
        Task<List<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<bool> CategoryExistsAsync(int categoryId);
    }
}