using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class GuaranteePeriod
    {
        public GuaranteePeriod()
        {
            RepairOrders = new HashSet<RepairOrder>();
        }

        public int GuaranteePeriodId { get; set; }
        public string GuaranteePeriodDesc { get; set; }
        public string GuaranteePeriodDescAr { get; set; }

        public virtual ICollection<RepairOrder> RepairOrders { get; set; }
    }
}
