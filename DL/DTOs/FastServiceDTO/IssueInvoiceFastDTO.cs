using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.FastServiceDTO
{
   public class IssueInvoiceFastDTO
    {
        public int FastServiceOrderId { get; set; }

        public int PaymentInvoiceType { get; set; }

        public int CategoryInvoiceId { get; set; }
        public bool  Discount { get; set; }
    }
}
