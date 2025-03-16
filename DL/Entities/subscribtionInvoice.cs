using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class SubscribtionInvoice : BaseDomain
    {
        public string InvoiceSerial { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }


        public int? UserId { get; set; }

        [Required]
        public string userFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }


        public int? WarshahId { get; set; }
        [Required]
        public string WarshahName { get; set; }

        public string WarshahTaxNumber { get; set; }
        [Required]
        public int PeriodSubscribtion { get; set; }

    
        public decimal SubscribtionWithoutVat { get; set; }
  
        public decimal SubscribtionVat { get; set; }
        [Required]
        public decimal TotalSubscribtion { get; set; }


        [Required]
        public string TransactionRef { get; set; }

        public int? TaxStatus { get; set; }

        public string QRCode { get; set; }

        public string PrevInvoiceHash { get; set; }
        public string DocUUID { get; set; }
        public string TypeTaxInvoice { get; set; }

        public string TaxErrorCode { get; set; }

        public int IntelCardCode { get; set; }

        public int InvoiceTypeId { get; set; }




    }
}
