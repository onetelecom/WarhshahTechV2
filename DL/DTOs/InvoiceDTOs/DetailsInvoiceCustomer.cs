using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class DetailsInvoiceCustomer
    {
        public string CarOwnerName { get; set; }

        public string CarType { get; set;}


        public Decimal Total { get; set; }

        public string InvoiceStatus { get; set; }

        public DateTime CreateOn { get; set; }

        public int InvoiceNumber { get; set; }

    }
}
