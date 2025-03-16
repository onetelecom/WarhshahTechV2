using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class ItemAndInvoice
    {
     
        public Invoice Invoice { get; set; }

        public HashSet<InvoiceItem> InvoiceItem{ get; set; }

    }
}
