using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryInBooks = new HashSet<CategoryInBook>();
        }

        public int CateId { get; set; }
        public string Name { get; set; } = null!;
        public string? Describe { get; set; }

        public virtual ICollection<CategoryInBook> CategoryInBooks { get; set; }
    }
}
