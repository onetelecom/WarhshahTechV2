using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InspectionDTOs
{
    public  class InspectionItemsDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(200)]
        public string ItemNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(200)]
        public string ItemNameEn { get; set; }

        [Required]
        public int? InspectionSectionId { get; set; }
        

        public bool? Status { get; set; } = false;

        public int? WarshahId { get; set; }
      

        public bool? IsCommon { get; set; } = false;

    }
}
