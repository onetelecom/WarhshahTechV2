using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class DebitAndCreditor : BaseDomain
    {
        [Required]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        [Required]
        public int NoticeNumber { get; set; }

        [Required]
        public string NoticeSerial { get; set; }

        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
        public int Flag { get; set; }

        public decimal TotalWithoutVat { get; set; }

        public decimal Vat { get; set; }

        public decimal Total  { get; set; }

        [Required]
        public int? PaymentTypeInvoiceId { get; set; }
        public virtual PaymentTypeInvoice PaymentTypeInvoice { get; set; }

        public bool? DiscountPoint { get; set; }

        public decimal? DiscPoint { get; set; }

        public decimal? FixingPrice { get; set; }
        public int? TaxStatus { get; set; }

        public string QRCode { get; set; }

        public string PrevInvoiceHash { get; set; }
        public string DocUUID { get; set; }

        public string TypeTaxInvoice { get; set; }
        public string TaxErrorCode { get; set; }


    }
}
