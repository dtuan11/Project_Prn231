using API.Models;

namespace API.DAO.IDAO
{
    public interface IChapterDao
    {
        Chapter GetChapterById(int id);
        void AddReading(Reading reading);
        Reading GetReading(int userId, int chapterId);
        Book GetBookById(int bookId);
    }
}
