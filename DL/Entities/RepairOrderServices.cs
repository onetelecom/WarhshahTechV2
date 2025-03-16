using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class RepairOrderServices:BaseDomain
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TechId { get; set; }
        public string Gruntee { get; set; }
    }
}
