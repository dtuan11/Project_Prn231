using API.DAO.IDAO;
using API.DTO.Request;
using API.DTO.Response;
using API.Models;
using API.Repositories.IRepositories;

namespace API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IReportDao _reportDao;
        private readonly IBookDao _bookDao;

        public ReportRepository(IReportDao reportDao, IBookDao bookDao)
        {
            _reportDao = reportDao;
            _bookDao = bookDao;
        }
        public ReportResponse GetReportFormData(int bookId)
        {
            var book = _bookDao.GetBookById(bookId);
            if (book == null)
            {
                return new ReportResponse
                {
                    Success = false,
                    Message = "Book not found"
                };
            }

            return new ReportResponse
            {
                BookTitle = book.Title,
                ReportTypes = _reportDao.GetReportTypes().Select(rt => rt.ReportType1).ToList(),
                Success = true
            };
        }

        public ReportResponse SubmitReport(ReportRequest request)
        {
            var lastReportTime = _reportDao.GetLastReportTime(request.UserId);
            if (lastReportTime != null && (DateTime.Now - lastReportTime.Value).TotalHours < 3)
            {
                return new ReportResponse
                {
                    Success = false,
                    Message = "You can only report once every 3 hours."
                };
            }

            var book = _bookDao.GetBookById(request.BookId);
            if (book == null)
            {
                return new ReportResponse
                {
                    Success = false,
                    Message = "Book not found"
                };
            }

            var reportType = _reportDao.GetReportTypes().FirstOrDefault(rt => rt.ReportType1 == request.ReportType);
            if (reportType == null)
            {
                return new ReportResponse
                {
                    Success = false,
                    Message = "Invalid report type"
                };
            }

            var report = new Report
            {
                UserId = request.UserId,
                BookId = request.BookId,
                Problem = reportType.ReportId,
                Chapter = request.Chapter.ToString(),
                Detail = request.Description,
                ReplyStatus = "Pending",
                ReportTime = DateTime.Now
            };

            _reportDao.AddReport(report);

            return new ReportResponse
            {
                Success = true,
                Message = "Feedback sent successfully!"
            };
        }
    }
}
