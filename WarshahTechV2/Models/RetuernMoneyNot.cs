using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class RetuernMoneyNot
    {
        public int Id { get; set; }
        public decimal ReturnMoney { get; set; }
        public decimal RemainMoney { get; set; }
        public decimal VatMoney { get; set; }
        public Guid InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
