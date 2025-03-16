using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.Sales
{
    public class SalesOfferDto:BaseDomainDTO
    {
        public int SalesReqId { get; set; }
        public decimal BuyPrice { get; set; }
        public string Notes { get; set; }
        public int SparePartTaseerId { get; set; }

    }
}
