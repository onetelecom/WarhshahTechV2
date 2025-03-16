using System;
using System.Collections.Generic;
using System.Text;

namespace KSAEinvoice
{

    public class TaxResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Reuslt { get; set; }
        public int StatusCode { get; set; }
        public object Response { get; set; } 
        public  string InvoiceXml { get; set; }
        public long InvoiceID { get; set; }
        public long InvoiceTrNo { get; set; }
        public  string InvoiceUUIDNew { get; set; }
        public  string InvoiceQr_CodeNew { get; set; }
        public  string InvoiceHashNew { get; set; }
        public  string PrevInvoiceHash { get; set; }
        public  int TaxStatus { get; set; }

    }

}


