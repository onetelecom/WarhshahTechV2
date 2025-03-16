using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class UserPermission:BaseDomain
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PermissionId { get; set; }
    }
}
