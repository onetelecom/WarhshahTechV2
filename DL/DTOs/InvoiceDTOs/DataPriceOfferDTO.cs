using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class DataPriceOfferDTO
    {
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

        public int? WarshahId { get; set; }

    }
}
