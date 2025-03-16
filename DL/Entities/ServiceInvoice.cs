using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
   public class ServiceInvoice:BaseDomain
    {
        public int WarshahId { get; set; }
        public decimal Total { get; set; }
      
        public decimal Vat { get; set; }
        
        public decimal BeforeDiscount { get; set; }
        public decimal Discount { get; set; }
        public decimal afterDiscount { get; set; }
       

        public int MotorsId { get; set; }
        public Motors Motors { get; set; }
        public int PaymentTypeInvoiceId { get; set; }
      
        public int TechId { get; set; }
        public User Tech { get; set; }

        public bool? DiscountPoint { get; set; }
         
        public decimal? DiscPoint { get; set; }
    }
}
