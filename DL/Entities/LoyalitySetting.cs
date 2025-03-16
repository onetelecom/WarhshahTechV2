using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class LoyalitySetting:BaseDomain
    {
        public int WarshahId { get; set; }
        public decimal LoyalityPointsPerCurrancy { get; set; }
    }
}
