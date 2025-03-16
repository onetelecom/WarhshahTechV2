using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.AddressDTOs
{
   public class CityDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(100)]
        public string CityNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string CityNameEn { get; set; }

        // Relationship  ( one Region  to many City ) 
        public int RegionId { get; set; }
    }
}
