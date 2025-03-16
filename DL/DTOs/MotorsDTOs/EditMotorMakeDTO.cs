using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.MotorsDTOs
{
   public class EditMotorMakeDTO : BaseDomainDTO
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string MakeNameAr { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string MakeNameEn { get; set; }

    }
}
