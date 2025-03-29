namespace API.DTO.Response
{
    public class UserBookViewResponse
    {
        public string? Username { get; set; }
        public int TotalCount { get; set; }
    }
    public class ViewStaticsResponse
    {
        public string? Title { get; set; }
        public int Views { get; set; }
    }
}
