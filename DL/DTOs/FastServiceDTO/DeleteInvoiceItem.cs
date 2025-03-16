using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.FastServiceDTO
{
   public class DeleteInvoiceItem
    {
        public int InvoiceId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
