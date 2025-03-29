using API.DTO.Request;
using API.DTO.Response;
using API.Models;

namespace API.Repositories.IRepositories
{
    public interface IBookRepository
    {
        List<BookResponse> GetApprovedBooks();
        List<BookResponse> GetNewBooksWithLatestChapters();
        List<CategoryResponse> GetCategories();
        List<BookResponse> GetBooksByCategory(int categoryId);
        bool CategoryExists(int categoryId);
        List<BookResponse> SearchBooks(string keyword);
        BookDetailResponse GetBookDetailById(int id);
        BookResponse CreateBook(BookRequest bookRequest);
        bool UpdateBook(int bookId, BookRequest request);
        bool DeleteBook(int bookId);
    }
}