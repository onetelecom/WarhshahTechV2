using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
   public class PaymentTypeInvoiceDTO
    {

        [Required]
        [MinLength(1), MaxLength(100)]
        public string PaymentTypeNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string PaymentTypeNameEn { get; set; }

    }
}
