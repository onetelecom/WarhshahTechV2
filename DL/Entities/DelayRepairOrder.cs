using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class DelayRepairOrder : BaseDomain
    {
        public int WarshahId { get; set; }
        public int DelayTime { get; set; }

    }
}
