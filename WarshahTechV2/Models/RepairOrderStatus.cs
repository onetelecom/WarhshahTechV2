using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class RepairOrderStatus
    {
        public RepairOrderStatus()
        {
            RepairOrders = new HashSet<RepairOrder>();
        }

        public int RepairOrderStatusId { get; set; }
        public string RepairOrderStatusDesc { get; set; }
        public string RepairOrderStatusDescAr { get; set; }

        public virtual ICollection<RepairOrder> RepairOrders { get; set; }
    }
}
