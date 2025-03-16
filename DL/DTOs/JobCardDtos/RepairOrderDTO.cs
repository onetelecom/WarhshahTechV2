using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.JobCardDtos
{ 
  public  class RepairOrderDTO:BaseDomainDTO
    {

       
        public decimal? KMOut { get; set; }
        [Required]
        public string TechReview { get; set; }
        [Required]
        public string Garuntee { get; set; }
        [Required]
        public decimal FixingPrice { get; set; }
        [Required]
        public decimal Deiscount { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }
        public int RepairOrderStatus { get; set; }
        [Required]
        public int WarshahId { get; set; }
        [Required]
        public int TechId { get; set; }
      
        public int? ReciptionOrderId { get; set; }
        public int? InspectionTemplateId { get; set; }
        public int? InspectionWarshahReportId { get; set; }

        public bool? DiscountPoint { get; set; }

    }
}
