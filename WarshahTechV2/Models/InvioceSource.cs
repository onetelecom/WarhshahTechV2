using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class InvioceSource
    {
        public InvioceSource()
        {
            Financials = new HashSet<Financial>();
        }

        public int Id { get; set; }
        public string InvioceSource1 { get; set; }
        public string InvioceTaxNo { get; set; }
        public Guid FinancialtypeId { get; set; }

        public virtual ICollection<Financial> Financials { get; set; }
    }
}
