using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InspectionDTOs
{
  public  class EditInspectionSectionDTO : BaseDomainDTO
    {

        [Required]
    
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string SectionNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(200)]
        public string SectionNameEn { get; set; }

      
        public int? InspectionTemplateId { get; set; }


        public int? WarshahId { get; set; }


        public bool? IsCommon { get; set; } = false;

    }
}
