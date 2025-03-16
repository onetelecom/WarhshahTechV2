using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class PriceOfferItemDTO
    {
        public int PriceOfferId { get; set; }
        public string SparePartNameAr { get; set; }
        public string SparePartNameEn { get; set; }
        public int Quantity { get; set; }
        public decimal PeacePrice { get; set; }

        public decimal FixingPrice { get; set; }
        public string Garuntee { get; set; }

    }
}
