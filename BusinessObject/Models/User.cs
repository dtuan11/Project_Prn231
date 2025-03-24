using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
            Comments = new HashSet<Comment>();
            Rates = new HashSet<Rate>();
            Readings = new HashSet<Reading>();
            Reports = new HashSet<Report>();
            Responses = new HashSet<Response>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? Transaction { get; set; }
        public string? Avatar { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
