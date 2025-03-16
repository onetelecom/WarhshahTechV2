using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ClaimInvoice : BaseDomain
    {

        public int InvoiceId { get; set; }
        public string InvoiceSerial { get; set; }

        public int InvoiceNumber { get; set; }

        public string CarOwnerName { get; set; }
        public string CarOwnerPhone { get; set; }

        public string CarOwnerID { get; set; }

        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }

        public int? WarshahId { get; set; }

    }
}
