using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class UnderCwarshah
    {
        public Guid WarshahId { get; set; }
        public string WarshahName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int UntiNo { get; set; }
        public decimal Vat { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
    }
}
