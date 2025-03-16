using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SparePartsDTOs
{
   public class CategoryTaseerSparePartsDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(500)]
        public string CategoryNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(500)]
        public string CategoryNameEn { get; set; }
    }
}
