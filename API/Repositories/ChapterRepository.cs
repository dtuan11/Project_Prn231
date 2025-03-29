using API.Models;
using API.DAO;
using API.Repositories.IRepositories;
using API.DAO.IDAO;
using API.DTO.Response;

namespace API.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly IChapterDao _chapterDAO;

        public ChapterRepository(IChapterDao chapterDAO)
        {
            _chapterDAO = chapterDAO;
        }

       
        public ChapterDetailResponse GetChapterDetails(int chapterId, int bookId, int? userId)
        {
            var chapter = _chapterDAO.GetChapterById(chapterId);
            var book = _chapterDAO.GetBookById(bookId);

            if (chapter == null || book == null)
            {
                return null;
            }

            if (userId.HasValue)
            {
                var reading = _chapterDAO.GetReading(userId.Value, chapterId);
                if (reading == null)
                {
                    _chapterDAO.AddReading(new API.Models.Reading
                    {
                        UserId = userId.Value,
                        Chapterid = chapterId,
                        Bookid = bookId,
                        ReadingDate = DateTime.Now,
                        Book = book,
                        Chapter = chapter
                    });
                }
            }

            return new ChapterDetailResponse
            {
                ChapterId = chapter.ChapterId,
                NumberChapter = chapter.NumberChapter,
                ChapterName = chapter.ChapterName,
                Contents1 = chapter.Contents1,
                Contents2 = chapter.Contents2,
                BookId = book.BookId,
                BookTitle = book.Title
            };
        }
    }
}