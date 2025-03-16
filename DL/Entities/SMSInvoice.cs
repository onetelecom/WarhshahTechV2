using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class SMSInvoice:BaseDomain
    {
        public decimal Total { get; set; }
        public decimal SuTotal { get; set; }
        public decimal Vat { get; set; }
        public string item { get; set; }
        public int  QTY { get; set; }
        public DateTime Date { get; set; }
        public string Serial { get; set; }
        public string TansactionId { get; set; }
        public int WarshahId { get; set; }

    }
}
