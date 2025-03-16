using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class InvoiceItemDTO
    {
        public int InvoiceId { get; set; }
        public string SparePartNameAr { get; set; }
        public string SparePartNameEn { get; set; }
        public int Quantity { get; set; }
        public decimal PeacePrice { get; set; }
        public string Garuntee { get; set; }
    }
}
