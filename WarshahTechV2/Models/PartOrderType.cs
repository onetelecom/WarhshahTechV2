using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class PartOrderType
    {
        public PartOrderType()
        {
            RepairOrders = new HashSet<RepairOrder>();
        }

        public int PartOrderTypeId { get; set; }
        public string OrderTypeName { get; set; }

        public virtual ICollection<RepairOrder> RepairOrders { get; set; }
    }
}
