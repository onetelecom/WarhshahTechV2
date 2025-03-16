using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Catch
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Date { get; set; }
        public string DocNumber { get; set; }
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string AmountType { get; set; }
        public string RefranceAmountType { get; set; }
        public string Discriptotion { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
