using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class InvocieCreditDTO
    {
        public Invoice Invoice { get; set; }
        public int CreditCount { get; set; }
        public int DebitCount { get; set; }
        public int AdvancedpaymentCount { get; set; }

    }
}
