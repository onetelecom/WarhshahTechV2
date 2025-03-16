using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public  class PaymentAndReceiptVoucher :BaseDomain
    {
       
        [Required]
        public int DocNumber { get; set; }
        [Required]
        public string ClientName { get; set; }

        [Required]
        public int TypeVoucher { get; set; }

        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
        public decimal? AdvancePayment { get; set; }
        [Required]
        public int PaymentTypeInvoiceId { get; set; }
        public PaymentTypeInvoice PaymentTypeInvoice { get; set; }
        [Required]
        public string Discriptotion { get; set; }


    }


}
