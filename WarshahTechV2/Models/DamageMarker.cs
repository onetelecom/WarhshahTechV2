using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class DamageMarker
    {
        public int DamageMarkerId { get; set; }
        public Guid? ReceptionistRepairOrderId { get; set; }
        public Guid? RepairOrderId { get; set; }
        public double? XPosition { get; set; }
        public double? YPosition { get; set; }
    }
}
