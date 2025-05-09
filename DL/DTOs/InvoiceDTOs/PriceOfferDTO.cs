﻿using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class PriceOfferDTO : BaseDomainDTO
    {
        public int OfferNumber { get; set; }

        public string CarOwnerName { get; set; }
        public string CarOwnerPhone { get; set; }

        public string CarOwnerTaxNumber { get; set; }
        public string CarOwnerCR { get; set; }

        public string CarOwnerAddress { get; set; }
        public int? CarOwnerID { get; set; }
        public string CarType { get; set; }

        public string CarPlateNo { get; set; }

        public int? MotorColorId { get; set; }


        public int? MotorMakeId { get; set; }


        public int? MotorModelId { get; set; }
     

        public int? MotorYearId { get; set; }
 

        public decimal FixingPrice { get; set; }
        public decimal Deiscount { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }

        [Required]
        public int? WarshahId { get; set; }
       

    }
}
