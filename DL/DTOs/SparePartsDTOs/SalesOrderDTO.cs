using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SparePartsDTOs
{
    public class SalesOrderDTO
    {

        public string CategorySparePartsId { get; set; }


        public string WarshahId { get; set; }

        public string SubCategoryPartsId { get; set; }

        public string MotorYearId { get; set; }


        public string MotorMakeId { get; set; }


        public string MotorModelId { get; set; }

        public string SparePartName { get; set; }

        public string PartDescribtion { get; set; }

        public string PartNum { get; set; }

        public string Quantity { get; set; }



        public decimal BuyingPrice { get; set; }



    }
}
