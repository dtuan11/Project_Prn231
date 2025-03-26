namespace API.DTO.Response
{
    public class ReportResponse
    {
        public string BookTitle { get; set; }
        public List<string> ReportTypes { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
