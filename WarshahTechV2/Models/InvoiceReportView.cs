using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class InvoiceReportView
    {
        public int InvoiceSerial { get; set; }
        public string InvoiceTypeDescAr { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string YearName { get; set; }
        public string CarOwnerName { get; set; }
        public string ColorName { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public string InvoiceStatusDescAr { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? InvoiceNumber { get; set; }
        public int RepairOrderSerial { get; set; }
        public Guid? WarshahId { get; set; }
    }
}
