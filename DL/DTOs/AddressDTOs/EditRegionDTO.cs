using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.AddressDTOs
{
   public class EditRegionDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string RegionNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string RegionNameEn { get; set; }

        // Relationship  ( one Country  to many Region ) 
        public int CountryId { get; set; }
    }
}
