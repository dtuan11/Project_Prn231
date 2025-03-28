using API.Models;

namespace API.DAO.IDAO
{
    public interface IBookDao
    {
        List<Book> GetApprovedBooks();
        List<Book> GetNewBooks();
        Dictionary<int, Chapter> GetLatestChapters(List<Book> books);
        List<Book> GetBooksByCategory(int categoryId);
        List<Category> GetCategories();
        bool CategoryExists(int categoryId);
        List<Book> SearchBooks(string keyword);
        Book GetBookById(int id);
        List<Rate> GetRatesByBookId(int bookId);

        void RateBook(int point , int bookId, int userId);

    }
}
