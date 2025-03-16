using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.AddressDTOs
{
   public class CountryDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(100)]
        public string CountryNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string CountryNameEn { get; set; }

    }
}
