using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class FastServiceInvoiceDTO
    {
        public int? PaymentTypeInvoiceId { get; set; }


        public int InvoiceTypeId { get; set; } = 3;
        public string CarOwnerId { get; set; }
        public string CarType { get; set; }
      
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }
        [Required]
        public int? WarshahId { get; set; }

    

      
        
     

    }
}
