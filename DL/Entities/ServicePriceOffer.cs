using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ServicePriceOffer : BaseDomain
    {
        public int PriceOfferId { get; set; }
        public PriceOffer PriceOffer { get; set; }
        public string ServiceName { get; set; }
        public decimal FixingPrice { get; set; }
        public string Garuntee { get; set; }

    }
}
