using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class ReceptionistRepairOrder
    {
        public Guid ReceptionistRepairOrderId { get; set; }
        public int MotorId { get; set; }
        public string MalfunctionDesc { get; set; }
        public decimal? CheckingPrice { get; set; }
        public Guid? WarshahId { get; set; }
        public Guid ReceptionistId { get; set; }
        public Guid TechnicianId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? RepairOrderId { get; set; }
        public string KmIn { get; set; }

        public virtual Motor Motor { get; set; }
    }
}
