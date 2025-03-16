using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Pricing
    {
        public int Id { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ChassaiNum { get; set; }
        public int Qty { get; set; }
        public string Describtion { get; set; }
        public Guid WharshaId { get; set; }
        public bool IsDone { get; set; }
    }
}
