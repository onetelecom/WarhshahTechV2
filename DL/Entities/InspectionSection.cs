using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public class InspectionSection : BaseDomain
    {

        [Required]
        [MinLength(1), MaxLength(200)]
        public string SectionNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(200)]
        public string SectionNameEn { get; set; }

        [Required]
        public int? InspectionTemplateId { get; set; }
        public InspectionTemplate InspectionTemplate { get; set; }

        public int? WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public bool? IsCommon { get; set; } = false;


    }
}
