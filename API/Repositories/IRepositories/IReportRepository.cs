using API.DTO.Request;
using API.DTO.Response;

namespace API.Repositories.IRepositories
{
    public interface IReportRepository
    {
        ReportResponse GetReportFormData(int bookId);
        ReportResponse SubmitReport(ReportRequest request);
        List<AdminReportResponse> GetAllReports();
        bool UpdateReportStatus(UpdateReportRequest request);
    }
}
