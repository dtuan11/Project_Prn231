using API.DAO.IDAO;
using API.Models;

namespace API.DAO
{
    public class ReportDao : IReportDao
    {
        private readonly PRN221_Project_1Context _context;

        public ReportDao(PRN221_Project_1Context context)
        {
            _context = context;
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

