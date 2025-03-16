using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Dashboard
    {
        public int DashboardId { get; set; }
        public int? RepairOrders { get; set; }
        public int? WarshahUsers { get; set; }
        public int? MotorOwners { get; set; }
        public decimal? PaidInvoiceTotal { get; set; }
    }
}
