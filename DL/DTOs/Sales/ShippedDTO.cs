using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.Sales
{
    public class ShippedDTO : BaseDomainDTO
    {
        public int Id { get; set; }
        public decimal SellPrice { get; set; }

        
        public decimal MarginPercent { get; set; }
    }
}
