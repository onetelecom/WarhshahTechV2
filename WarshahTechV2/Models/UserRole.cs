using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int? RoleId { get; set; }
        public Guid? UserId { get; set; }

        public virtual Role Role { get; set; }
    }
}
