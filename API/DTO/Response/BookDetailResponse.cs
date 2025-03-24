namespace API.DTO.Response
{
    public class BookDetailResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Detail { get; set; }
        public string Status { get; set; }
        public List<ChapterResponse> Chapters { get; set; } = new List<ChapterResponse>();
        public int RatePoint { get; set; }
        public int RateTime { get; set; }
    }
}
