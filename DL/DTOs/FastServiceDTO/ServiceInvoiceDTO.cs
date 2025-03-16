using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class ServiceInvoiceDTO : BaseDomainDTO
    {
        [Required]
        public int WarshahId { get; set; }
        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal Vat { get; set; }
        [Required]

        public decimal BeforeDiscount { get; set; }
        [Required]

        public decimal Discount { get; set; }
        [Required]

        public decimal afterDiscount { get; set; }

        [Required]
        public int MotorsId { get; set; }
        [Required]

        public int PaymentTypeInvoiceId { get; set; }
        [Required]

        public int TechId { get; set; }

        public bool? DiscountPoint { get; set; }

        public decimal? DiscPoint { get; set; }


    }
}
