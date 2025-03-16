using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.JobCardDtos
{
   public class EditRepairOrderDTO
    {
        [Required]
        public int Id { get; set; }

    
        public decimal? KMOut { get; set; }
     
        public string TechReview { get; set; }
     
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
        public bool AddedDiscountForSpareParts { get; set; }
        public bool AddedDiscountForFixingPrice { get; set; }


        public decimal DiscSpareMoney { get; set; }

        public decimal DiscFixingMoney { get; set; }

        public bool VatSpareParts { get; set; }
        public bool VatFixingPrice { get; set; }

        public decimal VatSpareMoney { get; set; }

        public decimal VatFixingMoney { get; set; }

        public decimal SparePartsTotal { get; set; }


        public bool? DiscPoint { get; set; }

        public decimal? DiscountPoint { get; set; }

    }
}
