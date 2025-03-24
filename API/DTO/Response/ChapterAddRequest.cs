namespace API.DTO.Response
{
    public class ChapterAddRequest
    {
        public int BookId { get; set; }
        public int NumberChapter { get; set; }
        public string Content { get; set; }
    }
}
