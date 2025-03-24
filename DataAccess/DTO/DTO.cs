using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    internal class DTO
    {
        public class UserDTO
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public int RoleId { get; set; }
            public bool Active { get; set; }
            public List<BookDTO> Books { get; set; }
        }

        public class BookDTO
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public string AuthorName { get; set; }
            public int? Views { get; set; }
            public string Status { get; set; }
            public List<CategoryDTO> Categories { get; set; }
        }

        public class CategoryDTO
        {
            public int CateId { get; set; }
            public string Name { get; set; }
        }

        public class ChapterDTO
        {
            public int ChapterId { get; set; }
            public int BookId { get; set; }
            public int? NumberChapter { get; set; }
            public string ChapterName { get; set; }
            public string Contents { get; set; } // Combined Contents1 and Contents2
        }

        public class RateDTO
        {
            public int RateId { get; set; }
            public int BookId { get; set; }
            public int UserId { get; set; }
            public int Point { get; set; }
        }

        public class ReportDTO
        {
            public int ReportId { get; set; }
            public int UserId { get; set; }
            public int BookId { get; set; }
            public string Problem { get; set; }
            public string Chapter { get; set; }
            public string Detail { get; set; }
            public string ReplyStatus { get; set; }
            public DateTime? ReportTime { get; set; }
        }
    }
}
