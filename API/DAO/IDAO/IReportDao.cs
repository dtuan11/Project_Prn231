using API.Models;

namespace API.DAO.IDAO
{
    public interface IReportDao
    {
        List<Report> GetAllReports();
        Report GetReportById(int reportId);
        void UpdateReport(Report report);
        List<ReportType> GetReportTypes();
        void AddReport(Report report);
        DateTime? GetLastReportTime(int userId);
    }
}
