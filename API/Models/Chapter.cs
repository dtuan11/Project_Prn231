using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            Readings = new HashSet<Reading>();
        }

        public int ChapterId { get; set; }
        public int BookId { get; set; }
        public int? NumberChapter { get; set; }
        public string? ChapterName { get; set; }
        public string? Contents1 { get; set; }
        public string? Contents2 { get; set; }
        public string? Status { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual ICollection<Reading> Readings { get; set; }
    }
}
