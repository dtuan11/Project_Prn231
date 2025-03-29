namespace API.DTO.Response
{
    public class ReadingResponse
    {
        public string BookTitle { get; set; }
        public string BookImage { get; set; }

        public int BookId { get; set; }
        public int ChapterId { get; set; }
        public string ChapterName { get; set; }
        public DateTime ReadingDate { get; set; }
    }
}
