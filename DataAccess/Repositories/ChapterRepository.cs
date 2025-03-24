using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ChapterDAO _chapterDAO;

        public ChapterRepository(ChapterDAO chapterDAO)
        {
            _chapterDAO = chapterDAO;
        }

        public async Task<List<Chapter>> GetChaptersByBookIdAsync(int bookId)
        {
            return await _chapterDAO.GetChaptersByBookIdAsync(bookId);
        }

        public async Task<(Chapter Chapter, Book Book)> GetChapterByIdAsync(int id, int bookId, int? userId)
        {
            var chapter = await _chapterDAO.GetChapterByIdAsync(id);
            var book = await _chapterDAO.GetBookByIdAsync(bookId);

            if (chapter == null || book == null)
            {
                return (null, null);
            }

            if (userId.HasValue)
            {
                var user = await _chapterDAO.GetUserByIdAsync(userId.Value);
                var reading = await _chapterDAO.GetReadingAsync(userId.Value, id);

                if (reading == null && user != null)
                {
                    await _chapterDAO.AddReadingAsync(new Reading
                    {
                        User = user,
                        Book = book,
                        Chapter = chapter,
                        Chapterid = chapter.ChapterId,
                        Bookid = book.BookId,
                        UserId = userId.Value,
                        ReadingDate = DateTime.Now
                    });
                }
            }

            return (chapter, book);
        }

        public async Task<Chapter> GetChapterByNumberAsync(int bookId, int numberChapter)
        {
            return await _chapterDAO.GetChapterByNumberAsync(bookId, numberChapter);
        }

        public async Task AddChapterAsync(Chapter chapter)
        {
            await _chapterDAO.AddChapterAsync(chapter);
        }

        public async Task UpdateChapterAsync(Chapter chapter)
        {
            await _chapterDAO.UpdateChapterAsync(chapter);
        }

        public async Task DeleteChapterAsync(Chapter chapter)
        {
            await _chapterDAO.DeleteChapterAsync(chapter);
        }
    }
}