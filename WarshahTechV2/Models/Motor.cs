using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Motor
    {
        public Motor()
        {
            ReceptionistRepairOrders = new HashSet<ReceptionistRepairOrder>();
            RepairOrders = new HashSet<RepairOrder>();
        }

        public int MotorId { get; set; }
        public string ChassisNo { get; set; }
        public string PlateNo { get; set; }
        public int MotorYearId { get; set; }
        public int MotorColorId { get; set; }
        public Guid MotorOwner { get; set; }
        public Guid WarshahId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual MotorColor MotorColor { get; set; }
        public virtual MotorYear MotorYear { get; set; }
        public virtual ICollection<ReceptionistRepairOrder> ReceptionistRepairOrders { get; set; }
        public virtual ICollection<RepairOrder> RepairOrders { get; set; }
    }
}
