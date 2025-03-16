using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class ServicePriceOfferDTO :  BaseDomainDTO
    {
        public int PriceOfferId { get; set; }
        public string ServiceName { get; set; }
        public decimal FixingPrice { get; set; }
        public string Garuntee { get; set; }


    }
}
