using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class MotorColor
    {
        public MotorColor()
        {
            Motors = new HashSet<Motor>();
        }

        public int MotorColorId { get; set; }
        public string CorlorName { get; set; }
        public string ColorNameAr { get; set; }

        public virtual ICollection<Motor> Motors { get; set; }
    }
}
