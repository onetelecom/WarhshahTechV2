using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Stock
    {
        public int StockId { get; set; }
        public int? WarshahId { get; set; }
        public int? SparePartId { get; set; }
        public int? Quantity { get; set; }
    }
}
