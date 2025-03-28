using API.Models;
using API.DAO;
using API.Repositories.IRepositories;
using API.DAO.IDAO;
using API.DTO.Response;
using API.DTO.Request;

namespace API.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly IChapterDao _chapterDAO;

        public ChapterRepository(IChapterDao chapterDAO)
        {
            _chapterDAO = chapterDAO;
        }

        public void AddChapter(AddChapterRequest request)
        {
            _chapterDAO.AddChapter(new Chapter
            {
                NumberChapter = request.NumberChapter,
                ChapterName = request.ChapterName,
                Contents1 = request.Contents1,
                Contents2 = request.Contents2,
            });
        }

        public void DeleteChapter(int chapterId)
        {
            _chapterDAO.DeleteChapter(chapterId);
        }

        public void EditChapter(EditChapterRequest request)
        {
            var chapter = new Chapter
            {
                ChapterId = request.ChapterId,
                NumberChapter = request.NumberChapter,
                Contents1 = request.Contents1,
                Contents2 = request.Contents2,
                ChapterName = request.ChapterName
            };
            _chapterDAO.UpdateChapter(chapter);
        }

        public List<ChapterDetailResponse> GetAllChapterByBookId(int bookId)
        {
            return _chapterDAO.GetAllChapterByBookId(bookId).Select(x=>new ChapterDetailResponse
            {
                ChapterId = x.ChapterId,
                BookId = x.BookId,
                ChapterName = x.ChapterName,
                Contents1 = x.Contents1,
                Contents2 = x.Contents2,
                NumberChapter = x.NumberChapter,

            }).ToList();
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