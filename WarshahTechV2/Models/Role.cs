using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleDesc { get; set; }
        public string RoleDescAr { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
