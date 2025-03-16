using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class OldInvoiceItem : BaseDomain
    {
        public Guid OldInvoiceId { get; set; }
        public OldInvoice OldInvoice { get; set; }
        public string SparePartNameAr { get; set; }
        public string SparePartNameEn { get; set; }
        public int Quantity { get; set; }
        public decimal PeacePrice { get; set; }
        public string Garuntee { get; set; }

        public decimal FixPrice { get; set; }
    }

}
