using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class FinancialType
    {
        public FinancialType()
        {
            Financials = new HashSet<Financial>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }
        public Guid? WarshahId { get; set; }

        public virtual ICollection<Financial> Financials { get; set; }
    }
}
