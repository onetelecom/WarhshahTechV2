using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class FastServiceType
    {
        public int Id { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeNameEn { get; set; }
        public Guid ServiceTypeWarshahId { get; set; }
        public decimal ServiceTypePrice { get; set; }
    }
}
