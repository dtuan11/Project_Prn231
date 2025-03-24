using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Rate
    {
        public int RateId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Point { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
