using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Service
    {
        public Service()
        {
            RepairOrderParts = new HashSet<RepairOrderPart>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public virtual ICollection<RepairOrderPart> RepairOrderParts { get; set; }
    }
}
