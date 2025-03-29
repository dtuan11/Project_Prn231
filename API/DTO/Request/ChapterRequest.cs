namespace API.DTO.Request
{
    public class EditChapterRequest
    {
        public int ChapterId { get; set; }
        public int? NumberChapter { get; set; }
        public string? ChapterName { get; set; }
        public string? Contents1 { get; set; }
        public string? Contents2 { get; set; }
    }
    public class AddChapterRequest
    {
        public int bookId {  get; set; }
        public int? NumberChapter { get; set; }
        public string? ChapterName { get; set; }
        public string? Contents1 { get; set; }
        public string? Contents2 { get; set; }

    }
}
