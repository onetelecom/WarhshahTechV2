using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
   public class RepairOrderSparePart:BaseDomain
    {
        public int RepairOrderId { get; set; }
        public RepairOrder RepairOrder { get; set; }
        public int SparePartId { get; set; }
        public SparePart SparePart { get; set; }
        public int TechId { get; set; }
        public User Tech { get; set; }
        public int Quantity { get; set; }
        public decimal PeacePrice { get; set; }
        public string Garuntee { get; set; }
        public decimal FixPrice { get; set; }
    }
}
