using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.Reports
{
    public class InvoiceTodayDTO
    {

        public int InvoiceNumber { get; set; }
        public string CarOwnerName { get; set; }
        public string CarOwnerPhone { get; set; }
        public decimal TotalWithoutVat { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
