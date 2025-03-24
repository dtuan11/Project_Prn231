using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Report
    {
        public Report()
        {
            Responses = new HashSet<Response>();
        }

        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Problem { get; set; }
        public string? Chapter { get; set; }
        public string? Detail { get; set; }
        public string? ReplyStatus { get; set; }
        public DateTime? ReportTime { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual ReportType ProblemNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Response> Responses { get; set; }
    }
}
