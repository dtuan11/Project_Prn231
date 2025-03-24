using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CategoryInBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CateId { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Category Cate { get; set; } = null!;
    }
}
