using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class PaymentOp
    {
        public int Id { get; set; }
        public string PaymentPackage { get; set; }
        public int PackagePrice { get; set; }
        public int PaymentMonthesCount { get; set; }
        public int PaymentTotal { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid WarshahId { get; set; }
        public Guid UserId { get; set; }
        public string TransactionRef { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? Numofbranches { get; set; }
        public bool Store { get; set; }
        public bool Api { get; set; }
        public bool Tecapp { get; set; }
        public bool Warshaadminapp { get; set; }
    }
}
