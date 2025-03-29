using API.DTO.Request;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpGet]
        public IActionResult GetAllReports()
        {
            var reports = _reportRepository.GetAllReports();
            return Ok(reports);
        }

        [HttpPut("status")]
        public IActionResult UpdateReportStatus([FromBody] UpdateReportRequest request)
        {
            var success = _reportRepository.UpdateReportStatus(request);
            if (!success)
            {
                return NotFound("Report not found.");
            }
            return Ok(new { success = true, message = "Report status updated successfully." });
        }
        [HttpGet("{bookId}")]
        public IActionResult GetReportFormData(int bookId)
        {
            var response = _reportRepository.GetReportFormData(bookId);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult SubmitReport([FromBody] ReportRequest request)
        {
            var response = _reportRepository.SubmitReport(request);
            if (!response.Success)
            {
                return BadRequest(new { success = false, message = response.Message });
            }
            return Ok(response);
        }
    }
}
