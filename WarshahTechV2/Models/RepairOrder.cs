using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class RepairOrder
    {
        public RepairOrder()
        {
            Invoices = new HashSet<Invoice>();
            RepairOrderParts = new HashSet<RepairOrderPart>();
        }

        public Guid RepairOrderId { get; set; }
        public int RepairOrderSerial { get; set; }
        public int MotorId { get; set; }
        public string MalfunctionDesc { get; set; }
        public string TechnicianMalfunctionDesc { get; set; }
        public decimal? CheckingPrice { get; set; }
        public decimal? FixingPrice { get; set; }
        public int? GuaranteePeriodId { get; set; }
        public int PartOrderTypeId { get; set; }
        public int RepairOrderStatusId { get; set; }
        public bool? SparePartsBroughtByWorkshopStore { get; set; }
        public Guid? WarshahId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public decimal? Discount { get; set; }
        public Guid? ReceptionistId { get; set; }
        public string KmOut { get; set; }

        public virtual GuaranteePeriod GuaranteePeriod { get; set; }
        public virtual Motor Motor { get; set; }
        public virtual PartOrderType PartOrderType { get; set; }
        public virtual RepairOrderStatus RepairOrderStatus { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<RepairOrderPart> RepairOrderParts { get; set; }
    }
}
