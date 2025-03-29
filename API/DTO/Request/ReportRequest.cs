namespace API.DTO.Request
{
    public class ReportRequest
    {
        public int BookId { get; set; }
        public string ReportType { get; set; }
        public int Chapter { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
