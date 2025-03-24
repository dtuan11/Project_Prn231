using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Response
    {
        public int ResponseId { get; set; }
        public int UserId { get; set; }
        public string Detail { get; set; } = null!;
        public DateTime ResponseTime { get; set; }
        public int? ReportId { get; set; }

        public virtual Report? Report { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
