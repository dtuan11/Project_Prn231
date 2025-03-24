using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Book
    {
        public Book()
        {
            CategoryInBooks = new HashSet<CategoryInBook>();
            Chapters = new HashSet<Chapter>();
            Comments = new HashSet<Comment>();
            Rates = new HashSet<Rate>();
            Readings = new HashSet<Reading>();
            Reports = new HashSet<Report>();
        }

        public int BookId { get; set; }
        public int? UserId { get; set; }
        public string? AuthorName { get; set; }
        public string Title { get; set; } = null!;
        public int? Views { get; set; }
        public string? Detail { get; set; }
        public string? Img { get; set; }
        public DateTime PublishDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Approve { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<CategoryInBook> CategoryInBooks { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
