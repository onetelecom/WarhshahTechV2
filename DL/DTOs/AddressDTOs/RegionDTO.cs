using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.AddressDTOs
{
  public  class RegionDTO : BaseDomainDTO
    {
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
