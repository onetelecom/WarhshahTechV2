using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
   public class TaseerItem:BaseDomain
    {
        public string PartName { get; set; }
        public string VinNo { get; set; }
        public string PartNumber { get; set; }
        public int QTY { get; set; }
        public int Status { get; set; }

        public DateTime? DelivaryDate { get; set; }
        public int RoId { get; set; }
        public int WarshahId { get; set; }

    }
}
