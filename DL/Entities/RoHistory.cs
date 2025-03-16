using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
   public class RoHistory:BaseDomain
    {
        public int RepairOrderId { get; set; }
        public RepairOrder RepairOrder { get; set; }

        public int StatusId { get; set; }
        public string  HistoryBody { get; set; }
    }
}
