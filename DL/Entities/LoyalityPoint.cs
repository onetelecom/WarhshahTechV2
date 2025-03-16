using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class LoyalityPoint:BaseDomain
    {
        public int WarshahId { get; set; }
        public decimal Points { get; set; }
        public int CarOwnerId { get; set; }
    }
}
