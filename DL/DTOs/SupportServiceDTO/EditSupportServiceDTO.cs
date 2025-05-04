using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SupportServiceDTO
{
   public class EditSupportServiceDTO : BaseDomainDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ServiceName { get; set; }

        public decimal? Price { get; set; }

        public double? WorkshopNetPercentage { get; set; }

        public double? WarchaTechShare { get; set; }

        public double? WorkshopShare { get; set; }
    }
}
