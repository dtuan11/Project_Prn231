using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface IChapterRepository
    {
        Task<List<Chapter>> GetChaptersByBookIdAsync(int bookId);
        Task<(Chapter Chapter, Book Book)> GetChapterByIdAsync(int id, int bookId, int? userId);
        Task<Chapter> GetChapterByNumberAsync(int bookId, int numberChapter);
        Task AddChapterAsync(Chapter chapter);
        Task UpdateChapterAsync(Chapter chapter);
        Task DeleteChapterAsync(Chapter chapter);
    }
}