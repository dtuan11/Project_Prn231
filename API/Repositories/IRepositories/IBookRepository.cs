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
    }
}