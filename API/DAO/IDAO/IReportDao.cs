using API.Models;

namespace API.DAO.IDAO
{
    public interface IReportDao
    {
        List<ReportType> GetReportTypes();
        void AddReport(Report report);
        DateTime? GetLastReportTime(int userId);
    }
}
