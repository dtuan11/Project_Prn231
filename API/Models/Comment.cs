using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Comment
    {
        public int CmtId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Cmt { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public int? Like { get; set; }
        public int? Reply { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
