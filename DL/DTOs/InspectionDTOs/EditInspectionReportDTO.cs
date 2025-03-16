using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InspectionDTOs
{
   public class EditInspectionReportDTO
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public int InspectionWarshahReportId { get; set; }


        [Required]
        [MinLength(1), MaxLength(200)]
        public string ItemNameAr { get; set; }

        [MinLength(1), MaxLength(200)]
        public string ItemNameEn { get; set; }

        public bool? Excellent { get; set; } = false;
        public bool? VeryGood { get; set; } = false;
        public bool? Good { get; set; } = false;

    }
}
