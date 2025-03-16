using DL.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Net.WebRequestMethods;

namespace DL.Entities
{
    public class Invoice : BaseDomain
    {

        public string InvoiceSerial { get; set; }

        public int InvoiceNumber { get; set; }
        public int InvoiceStatusId { get; set; }
        public int InvoiceTypeId { get; set; }

        public int? RepairOrderId { get; set; }
        public RepairOrder RepairOrder { get; set; }

        public string CarOwnerName { get; set; }
        public string CarOwnerPhone { get; set; }

        public string CarOwnerTaxNumber { get; set; }
        public string CarOwnerCR { get; set; }
        
        public string CarOwnerID { get; set; }
        public string CarOwnerCivilId { get; set; }

        public decimal? KMOut { get; set; }
        public int FastServiceOrderId { get; set; }

        public string TechReview { get; set; }


        public int? ReciptionOrderId { get; set; }
        public ReciptionOrder ReciptionOrder { get; set; }

        public decimal? KMIn { get; set; }
        public string CarType { get; set; }
        public decimal? CheckPrice { get; set; }
        public decimal FixingPrice { get; set; }
        public decimal Deiscount { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }
        public decimal? AdvancePayment { get; set; }
        public decimal? RemainAmount { get; set; }
        [Required]
        public int? WarshahId { get; set; }

        public string WarshahName { get; set; }
        public string WarshahCR { get; set; }
        public string WarshahTaxNumber { get; set; }
        public string WarshahPhone { get; set; }
        public string WarshahCity { get; set; }
        public string WarshahDescrit { get; set; }
        public string WarshahStreet { get; set; }
        public string WarshahAddress { get; set; }
        public string WarshahPostCode { get; set; }

        public string WarhshahCondition { get; set; }

        [Required]
        public int? PaymentTypeInvoiceId { get; set; }
        public virtual PaymentTypeInvoice PaymentTypeInvoice { get; set; }
        public string PaymentTypeName { get; set; }

        public string CarOwnerAddress { get; set; }

        
        public int? InspectionWarshahReportId { get; set; }
        public InspectionWarshahReport InspectionWarshahReport { get; set; }

        public decimal DiscSpareMoney { get; set; }

        public decimal DiscFixingMoney { get; set; }


        public decimal VatSpareMoney { get; set; }

        public decimal VatFixingMoney { get; set; }

        public int? InvoiceCategoryId { get; set; }


        
        public decimal? DiscountPoint { get; set; }

        public int? TaxStatus { get; set; }

        public string QRCode { get; set; }

        public string PrevInvoiceHash { get; set; }
        public string DocUUID { get; set; }
        public string TypeTaxInvoice { get; set; }

        public string TaxErrorCode { get; set; }
        




    }
}
