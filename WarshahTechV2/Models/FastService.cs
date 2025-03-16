using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class FastService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public decimal Price { get; set; }
        public Guid WarshahId { get; set; }
        public Guid RepairOrderId { get; set; }
    }
}
