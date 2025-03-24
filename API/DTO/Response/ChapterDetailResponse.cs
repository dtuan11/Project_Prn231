namespace API.DTO.Response
{
    public class ChapterDetailResponse
    {
        public int ChapterId { get; set; }
        public int? NumberChapter { get; set; }
        public string ChapterName { get; set; }
        public string Contents1 { get; set; }
        public string Contents2 { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
    }
}
