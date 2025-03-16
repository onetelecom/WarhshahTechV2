using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public class CreditorNotice : BaseDomain
    {

        [Required]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }
        [Required]
        public decimal ReturnMoney { get; set; }
        public decimal ReturnVat { get; set; }
        public decimal Total { get; set; }

    }
}
