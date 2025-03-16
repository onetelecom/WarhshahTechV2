using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class WarshahTechService : BaseDomain
    {

        public int WarshahId { get; set; }

        public int InvoiceId { get; set; }


        public DateTime InvoiceDate { get; set; }
        public string InvoiceSerial { get; set; }

        public string WarshahName { get; set; }

        public string WarshahPhone { get; set; }
        public int InvoiceNumber { get; set; }

        public int? PaymentTypeInvoiceId { get; set; }
        public string PaymentTypeName { get; set; }


        public decimal AfterDiscount { get; set; }

        public decimal WarshahService { get; set; }

    }
}
