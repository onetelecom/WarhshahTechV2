using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Client
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Idiqama { get; set; }
        public string Nationality { get; set; }
        public int? AccountStatusId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CustomerId { get; set; }
        public bool? ActivatedByMail { get; set; }
        public bool? LoggedIn { get; set; }
    }
}
