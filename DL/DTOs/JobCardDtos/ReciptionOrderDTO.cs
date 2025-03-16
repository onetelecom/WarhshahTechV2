using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.JobCardDtos
{
   public class ReciptionOrderDTO:BaseDomainDTO
    {
        [Required]
        public int CarOwnerId { get; set; }
        [Required]
        public int MotorsId { get; set; }
        [Required]
        public int ReciptionId { get; set; }
        [Required]
        public int TecnicanId { get; set; }
        public decimal? KM_In { get; set; }
        public decimal? AdvancePayment { get; set; }
        public decimal? CheckPrice { get; set; }
        [Required]
        public string CarOwnerDescribtion { get; set; }
        [Required]
        public string ReciptionDescribtion { get; set; }
        [Required]
        public int warshahId { get; set; }
        [Required]

        public bool IsCheck { get; set; }
     
        public int? PaymentTypeInvoiceId { get; set; }

        public int? InvoiceCategoryId { get; set; }

    }
}
