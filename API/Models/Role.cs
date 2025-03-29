using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
