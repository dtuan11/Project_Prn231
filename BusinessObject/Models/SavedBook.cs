using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class SavedBook
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
