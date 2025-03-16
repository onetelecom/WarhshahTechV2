using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class DebitNotice : BaseDomain
    {
       

        [Required]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
       
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public decimal Total { get; set; }
        




    }
}
