using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class ViewOilTransaction
    {
        public int Idkey { get; set; }
        public Guid IdreceptionOrder { get; set; }
        public int OilTypeId { get; set; }
        public decimal? OilPrice { get; set; }
        public int Quentity { get; set; }
        public Guid WarshahId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string OilTypeArName { get; set; }
        public string OilTypeEnName { get; set; }
    }
}
