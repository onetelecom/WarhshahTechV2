using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Financial
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid FinancialTypeId { get; set; }
        public decimal Ammount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InvoiceSerial { get; set; }
        public Guid WarshahId { get; set; }
        public int? InvoiceSourceId { get; set; }

        public virtual FinancialType FinancialType { get; set; }
        public virtual InvioceSource InvoiceSource { get; set; }
    }
}
