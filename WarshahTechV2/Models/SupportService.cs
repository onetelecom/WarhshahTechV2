using KSAEinvoice;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class SupportService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public decimal? Price { get; set; }

        public double? WorkshopNetPercentage { get; set; }

        public double? WarchaTechShare { get; set; }

        public double? WorkshopShare { get; set; }
    }
}
