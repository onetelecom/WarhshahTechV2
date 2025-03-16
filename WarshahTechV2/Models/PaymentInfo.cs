using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class PaymentInfo
    {
        public int Id { get; set; }
        public string PaymentPackage { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentMcount { get; set; }
        public DateTime PaymentEnd { get; set; }
        public decimal PaymentTotal { get; set; }
        public string PaymentMethod { get; set; }
        public Guid WarshahId { get; set; }
        public decimal PackageAmount { get; set; }
        public int? NumofBranches { get; set; }
        public bool Store { get; set; }
        public bool Apis { get; set; }
        public bool Tecapp { get; set; }
        public bool Waeshaadmapp { get; set; }
    }
}
