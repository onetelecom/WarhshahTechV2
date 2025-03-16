using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class InvoiceType
    {
        public InvoiceType()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int InvoiceTypeId { get; set; }
        public string InvoiceTypeDesc { get; set; }
        public string InvoiceTypeDescAr { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
