namespace API.DTO.Response
{
    public class BookResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        public DateTime PublishDate { get; set; }
        public LatestChapterResponse LatestChapter { get; set; }
    }
}
