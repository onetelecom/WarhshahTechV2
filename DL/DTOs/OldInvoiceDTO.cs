using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs
{
    public class OldInvoiceDTO 
    {
        public Guid Id { get; set; }
        public string InvoiceSerial { get; set; }

        public int InvoiceNumber { get; set; }
        public int InvoiceStatusId { get; set; }
        public int InvoiceTypeId { get; set; }

        public Guid? RepairOrderId { get; set; }
       

        public string CarOwnerName { get; set; }
        public string CarOwnerPhone { get; set; }

        public string CarOwnerID { get; set; }
        public string CarOwnerCivilId { get; set; }

        public decimal? KMOut { get; set; }
        public int FastServiceOrderId { get; set; }

        public string TechReview { get; set; }


        public Guid? ReciptionOrderId { get; set; }
      

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
        public Guid? WarshahId { get; set; }

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


        public string CarOwnerAddress { get; set; }

        public int? InvoiceCategoryId { get; set; }

        public DateTime OldCreatedon { get; set; }

    }
}
