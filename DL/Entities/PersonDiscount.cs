using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class PersonDiscount:BaseDomain
    {
        public int CarOwnerId { get; set; }
        public int DiscountPercentageForFixingPrice { get; set; }
        public int DiscountPercentageForSpareParts { get; set; }
        public int WarshahId { get; set; }

    }
}
