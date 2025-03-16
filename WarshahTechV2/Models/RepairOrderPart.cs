using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class RepairOrderPart
    {
        public int RepairOrderPartId { get; set; }
        public Guid? RepairOrderId { get; set; }
        public int? SparePartId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public int? QuantityAfter { get; set; }
        public decimal? PeacePrice { get; set; }
        public bool? InvoiceDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public string Gurantee { get; set; }

        public virtual RepairOrder RepairOrder { get; set; }
        public virtual Service Service { get; set; }
        public virtual SparePart SparePart { get; set; }
    }
}
