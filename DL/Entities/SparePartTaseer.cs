using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class SparePartTaseer : BaseDomain
    {
        [Required]
        public int CategorySparePartsId { get; set; }
        public CategorySpareParts CategorySpareParts { get; set; }
    
       
        [Required]
        public int SubCategoryPartsId { get; set; }
        public SubCategoryParts SubCategoryParts { get; set; }

        public int? TaseerSupplierId { get; set; }
        public TaseerSupplier TaseerSupplier { get; set; }


        [Required]
        public int MotorYearId { get; set; }
        public MotorYear motorYear { get; set; }
        [Required]
        public int MotorMakeId { get; set; }
        public MotorMake MotorMake { get; set; }
        [Required]
        public int MotorModelId { get; set; }
        public MotorModel MotorModel { get; set; }
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
