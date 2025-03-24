using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ReportType
    {
        public ReportType()
        {
            Reports = new HashSet<Report>();
        }

        public int ReportId { get; set; }
        public string ReportType1 { get; set; } = null!;

        public virtual ICollection<Report> Reports { get; set; }
    }
}
