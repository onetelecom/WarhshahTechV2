 using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class SalesRequest:BaseDomain
    {
        public int? MotorId { get; set; }

        public string CarDescribtion { get; set; }
        public int QTY { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Status { get; set; }
        public int WarshahId { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int? ROID { get; set; }
        public int? SalesRequestListId  { get; set; }
        public SalesRequestList SalesRequestList { get; set; }
        public int? SparePartTaseerId { get; set; }
        public SparePartTaseer SparePartTaseer { get; set; }
        public bool FromWarshah { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }

        public string Type { get; set; }
    }
}
