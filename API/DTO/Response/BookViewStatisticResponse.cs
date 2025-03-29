namespace API.DTO.Response
{
    public class BookViewStatisticResponse
    {
        public List<string> Titles { get; set; }
        public List<int> ViewCounts { get; set; }
    }
}
