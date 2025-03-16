using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SparePartsDTOs
{
    public class SparePartDTO :  BaseDomainDTO
    {
       
        public int? CategorySparePartsId { get; set; }

        public int? SupplierId { get; set; }
      
        public int? WarshahId { get; set; }
       
        public int? SubCategoryPartsId { get; set; }
      
        [Required]
        public int MotorYearId { get; set; }
     
        [Required]
        public int MotorMakeId { get; set; }
       
        [Required]
        public int MotorModelId { get; set; }
       
        [Required]
        [MinLength(1), MaxLength(200)]
        public string SparePartName { get; set; }
        
        public string PartDescribtion { get; set; }
     
        public string PartNum { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PeacePrice { get; set; }

        [Required]
        public decimal MarginPercent { get; set; }

        [Required]
        public decimal BuyingPrice { get; set; }

        [Required]
        public int MinimumRecordQuantity { get; set; }



        public decimal? BuyBeforeVat { get; set; }


        public decimal? SellBeforeVat { get; set; }
        public decimal? VatBuy { get; set; }


        public decimal? VatSell { get; set; }
    }
}
