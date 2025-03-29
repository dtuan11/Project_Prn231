namespace API.DTO.Response
{
    public class AdminReportResponse
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Problem { get; set; }
        public string Chapter { get; set; }
        public string Detail { get; set; }
        public string ReplyStatus { get; set; }
        public DateTime? ReportTime { get; set; }
    }
}
