using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
   public class DebitAndCreditorDTO : BaseDomainDTO
    {
        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int NoticeNumber { get; set; }
        [Required]
        public string NoticeSerial { get; set; }

        public int WarshahId { get; set; }
        
        [Required]
        public int Flag { get; set; }

        public decimal TotalWithoutVat { get; set; }

        public decimal Vat { get; set; }

        public decimal Total { get; set; }

        public int? PaymentTypeInvoiceId { get; set; }

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
