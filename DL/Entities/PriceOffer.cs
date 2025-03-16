using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class PriceOffer : BaseDomain
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
        public MotorColor MotorColor { get; set; }

        public int? MotorMakeId { get; set; }
        public MotorMake MotorMake { get; set; }

        public int? MotorModelId { get; set; }
        public MotorModel MotorModel { get; set; }

        public int? MotorYearId { get; set; }
        public MotorYear MotorYear { get; set; }

        public decimal FixingPrice { get; set; }
        public decimal Deiscount { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }
      
        [Required]
        public int? WarshahId { get; set; }

        public Warshah Warshah { get; set; }

    }
}
