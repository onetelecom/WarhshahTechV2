using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.MotorsDTOs
{
  public  class MotorMakeDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(100)]
        public string MakeNameAr { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string MakeNameEn { get; set; }
    }
}
