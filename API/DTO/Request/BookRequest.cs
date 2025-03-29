using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Request
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Img { get; set; }
        public string AuthorName { get; set; }
        public string Detail { get; set; }
        public string Status { get; set; }
        public string Approve { get; set; }
    }
}
