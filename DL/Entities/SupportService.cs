using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class SupportService : BaseDomain
    {
        [Required]
        [MinLength(1), MaxLength(100)]
        public string ServiceName { get; set; }

        public decimal? Price { get; set; }

        public double? WorkshopNetPercentage { get; set; }

        public double? WarchaTechShare { get; set; }

        public double? WorkshopShare { get; set; }
    }
}
