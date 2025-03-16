using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class InspectionItem : BaseDomain
    {
        [Required]
        [MinLength(1), MaxLength(200)]
        public string ItemNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(200)]
        public string ItemNameEn { get; set; }

        [Required]
        public int? InspectionSectionId { get; set; }
        public InspectionSection InspectionSection { get; set; }

        public bool? Status { get; set; } = true;

        public int? WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public bool? IsCommon { get; set; } = false;

    }
}
