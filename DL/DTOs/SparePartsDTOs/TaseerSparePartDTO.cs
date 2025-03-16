using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SparePartsDTOs
{
    public class TaseerSparePartDTO :  BaseDomainDTO
    {
        [Required]
        public int CategorySparePartsId { get; set; }


        [Required]
        public int SubCategoryPartsId { get; set; }

        public int? TaseerSupplierId { get; set; }


        [Required]
        public int MotorYearId { get; set; }
        [Required]
        public int MotorMakeId { get; set; }
        [Required]
        public int MotorModelId { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string SparePartName { get; set; }
        [Required]
        [MinLength(1), MaxLength(5000)]
        public string PartDescribtion { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string PartNum { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PeacePrice { get; set; }
        [Required]
        public decimal BuyingPrice { get; set; }

    }
}
