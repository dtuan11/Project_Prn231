using API.Models;
using API.DAO;
using API.Repositories.IRepositories;
using API.DAO.IDAO;
using API.DTO.Response;

namespace API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IBookDao _bookDao;

        public BookRepository(IBookDao bookDao)
        {
            _bookDao = bookDao;
        }

        public List<BookResponse> GetApprovedBooks()
        {
            var books = _bookDao.GetApprovedBooks();
            return books.Select(b => new BookResponse
            {
                BookId = b.BookId,
                Title = b.Title,
                Img = b.Img,
                Status = b.Status,
                PublishDate = b.PublishDate
            }).ToList();
        }

        public List<BookResponse> GetNewBooksWithLatestChapters()
        {
            var books = _bookDao.GetNewBooks();
            var latestChapters = _bookDao.GetLatestChapters(books);

            return books.Select(b => new BookResponse
            {
                BookId = b.BookId,
                Title = b.Title,
                Img = b.Img,
                Status = b.Status,
                PublishDate = b.PublishDate,
                LatestChapter = new LatestChapterResponse
                {
                    ChapterId = latestChapters[b.BookId].ChapterId,
                    NumberChapter = latestChapters[b.BookId].NumberChapter
                }
            }).ToList();
        }

        public List<CategoryResponse> GetCategories()
        {
            var categories = _bookDao.GetCategories();
            return categories.Select(c => new CategoryResponse
            {
                CateId = c.CateId,
                Name = c.Name
            }).ToList();
        }
        public List<BookResponse> GetBooksByCategory(int categoryId)
        {
            var books = _bookDao.GetBooksByCategory(categoryId);
            return books.Select(b => new BookResponse
            {
                BookId = b.BookId,
                Title = b.Title,
                Img = b.Img,
                Status = b.Status
            }).ToList();
        }
        public bool CategoryExists(int categoryId)
        {
            return _bookDao.CategoryExists(categoryId);
        }
        public List<BookResponse> SearchBooks(string keyword)
        {
            var books = _bookDao.SearchBooks(keyword);
            return books.Select(b => new BookResponse
            {
                BookId = b.BookId,
                Title = b.Title,
                Img = b.Img,
                Status = b.Status
            }).ToList();
        }
        public BookDetailResponse GetBookDetailById(int id)
        {
            var book = _bookDao.GetBookById(id);
            if (book == null)
            {
                return null;
            }

            var rates = _bookDao.GetRatesByBookId(id);
            var rateList = rates.Select(r => r.Point).ToList();
            var rateTime = rateList.Count;
            var ratePoint = 0;
            foreach (var rate in rateList)
            {
                ratePoint += rate;
            }
            if (rateList.Count > 0)
            {
                ratePoint = ratePoint / rateTime;
            }
            else
            {
                ratePoint= 0;
            }
            //var ratePoint = rateTime > 0 ? rateList.Sum() / rateTime : 0;

            return new BookDetailResponse
            {
                BookId = book.BookId,
                Title = book.Title,
                Img = book.Img,
                Detail = book.Detail,
                Status = book.Status,
                Chapters = book.Chapters.Select(c => new ChapterResponse
                {
                    ChapterId = c.ChapterId,
                    NumberChapter = c.NumberChapter,
                    ChapterName = c.ChapterName
                }).ToList(),
                RatePoint = ratePoint,
                RateTime = rateTime
            };
        }

        public void RateBook(int point, int bookId, int userId)
        {
            _bookDao.RateBook(point, bookId, userId);
        }
    }
}