using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class SparePart : BaseDomain
    {
       
        public int? CategorySparePartsId { get; set; }
        public CategorySpareParts CategorySpareParts { get; set; }
    
        public int? WarshahId { get; set; }
        public Warshah Warshah { get; set; }
      
        public int? SubCategoryPartsId { get; set; }
        public SubCategoryParts SubCategoryParts { get; set; }

        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }


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
