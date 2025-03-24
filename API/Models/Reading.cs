using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Reading
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Bookid { get; set; }
        public int Chapterid { get; set; }
        public DateTime ReadingDate { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Chapter Chapter { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
