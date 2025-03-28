using API.DAO.IDAO;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAO
{
    public class ReportDao : IReportDao
    {
        private readonly PRN221_Project_1Context _context;

        public ReportDao(PRN221_Project_1Context context)
        {
            _context = context;
        }
        public List<Report> GetAllReports()
        {
            return _context.Reports
                .Include(r => r.User)
                .Include(r => r.Book)
                .Include(r => r.ProblemNavigation)
                .ToList();
        }

        public Report GetReportById(int reportId)
        {
            return _context.Reports
                .Include(r => r.User)
                .Include(r => r.Book)
                .Include(r => r.ProblemNavigation)
                .FirstOrDefault(r => r.ReportId == reportId);
        }

        public void UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            _context.SaveChanges();
        }
        public List<ReportType> GetReportTypes()
        {
            return _context.ReportTypes.ToList();
        }

        public void AddReport(Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();
        }

        public DateTime? GetLastReportTime(int userId)
        {
            return _context.Reports
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.ReportTime)
                .Select(r => r.ReportTime)
                .FirstOrDefault();
        }
    }
}

