using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class InvoiceStatus
    {
        public InvoiceStatus()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int InvoiceStatusId { get; set; }
        public string InvoiceStatusDesc { get; set; }
        public string InvoiceStatusDescAr { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
