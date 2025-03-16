using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            RetuernMoneyNots = new HashSet<RetuernMoneyNot>();
        }

        public Guid InvoiceId { get; set; }
        public int InvoiceSerial { get; set; }
        public int? InvoiceTypeId { get; set; }
        public Guid RepairOrderId { get; set; }
        public decimal? CarCheckPrice { get; set; }
        public decimal? RepairPrice { get; set; }
        public decimal? SparePartsPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public int InvoiceStatusId { get; set; }
        public Guid? WarshahId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public int? InvoiceNumber { get; set; }

        public virtual InvoiceStatus InvoiceStatus { get; set; }
        public virtual InvoiceType InvoiceType { get; set; }
        public virtual RepairOrder RepairOrder { get; set; }
        public virtual ICollection<RetuernMoneyNot> RetuernMoneyNots { get; set; }
    }
}
