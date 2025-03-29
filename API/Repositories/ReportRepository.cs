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
        public List<AdminReportResponse> GetAllReports()
        {
            var reports = _reportDao.GetAllReports();
            return reports.Select(r => new AdminReportResponse
            {
                ReportId = r.ReportId,
                UserId = r.UserId,
                UserName = r.User.UserName,
                BookId = r.BookId,
                BookTitle = r.Book.Title,
                Problem = r.ProblemNavigation.ReportType1,
                Chapter = r.Chapter,
                Detail = r.Detail,
                ReplyStatus = r.ReplyStatus,
                ReportTime = r.ReportTime
            }).ToList();
        }

        public bool UpdateReportStatus(UpdateReportRequest request)
        {
            var report = _reportDao.GetReportById(request.ReportId);
            if (report == null)
            {
                return false;
            }

            report.ReplyStatus = request.ReplyStatus;
            _reportDao.UpdateReport(report);
            return true;
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
