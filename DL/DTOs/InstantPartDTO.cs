using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs
{
    public class InstantPartDTO : BaseDomainDTO
    {
        public int? WarshahId { get; set; }
        [Required]
        public string SparePartName { get; set; }
        public string PartDescribtion { get; set; }
        [Required]
        public string PartNum { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal BuyingPrice { get; set; }
        public decimal? BuyBeforeVat { get; set; }
        public decimal? VatBuy { get; set; }

        [Required]
        public decimal PeacePrice { get; set; }
        public decimal? VatSell { get; set; }
        public decimal? SellBeforeVat { get; set; }


    }
}
