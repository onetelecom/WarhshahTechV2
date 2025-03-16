using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SparePartsDTOs
{
   public class EditSubCategoryPartsDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(500)]
        public string SubCategoryNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(500)]
        public string SubCategoryNameEn { get; set; }

        // Relationship  ( one Category  to many SubCategory ) 
        public int CategorySparePartsId { get; set; }
    }
}
