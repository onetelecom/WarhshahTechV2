using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class OilTransaction
    {
        public int Idkey { get; set; }
        public Guid IdreceptionOrder { get; set; }
        public int OilTypeId { get; set; }
        public Guid WarshahId { get; set; }
        public int Quentity { get; set; }
        public decimal? OilPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual OilType OilType { get; set; }
    }
}
