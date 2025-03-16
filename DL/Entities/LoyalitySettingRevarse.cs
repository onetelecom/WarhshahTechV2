using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class LoyalitySettingRevarse : BaseDomain
    {
        public int WarshahId { get; set; }

        public int NoofPoints { get; set; }

        public decimal CurrancyPerLoyalityPoints { get; set; }
    }
}
