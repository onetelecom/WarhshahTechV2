using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InspectionDTOs
{
  public  class InspectionTemplateDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(200)]
        public string InspectionNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(200)]
        public string InspectionNameEn { get; set; }

        [Required]

        public decimal Price { get; set; }



        public int Hours { get; set; }
        public int? WarshahId { get; set; }

        public bool? IsCommon { get; set; } = false;

    }
}
