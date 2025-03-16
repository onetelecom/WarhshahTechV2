using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class UsersReportView
    {
        public string FullName { get; set; }
        public string TitleNameAr { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Nationality { get; set; }
        public string RoleDescAr { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? WarshahId { get; set; }
    }
}
