using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class RepairOrderReportView
    {
        public string CarName { get; set; }
        public string YearName { get; set; }
        public string RepairOrderStatus { get; set; }
        public string MalfunctionDesc { get; set; }
        public string TechnicianMalfunctionDesc { get; set; }
        public decimal? CheckingPrice { get; set; }
        public decimal? FixingPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedByName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public decimal? Discount { get; set; }
        public string ClientName { get; set; }
        public string ReciptionistName { get; set; }
        public int RepairOrderSerial { get; set; }
        public string WarshahName { get; set; }
        public Guid? WarshahId { get; set; }
    }
}
