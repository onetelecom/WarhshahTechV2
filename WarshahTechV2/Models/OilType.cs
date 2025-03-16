using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class OilType
    {
        public OilType()
        {
            OilTransactions = new HashSet<OilTransaction>();
        }

        public int OilTypeId { get; set; }
        public string OilArabicName { get; set; }
        public string OilEnglishName { get; set; }
        public string OilTypeArName { get; set; }
        public string OilTypeEnName { get; set; }
        public decimal OilPrice { get; set; }
        public Guid WarshahId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Warshah Warshah { get; set; }
        public virtual ICollection<OilTransaction> OilTransactions { get; set; }
    }
}
