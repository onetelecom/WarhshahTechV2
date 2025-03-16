using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.Sales
{

     public class EditSalesInvoiceDTO : BaseDomainDTO
    {
        [Required]

        public  int Id { get; set; }  
        public string InvoiceSerial { get; set; }
        public int InvoiceNumber { get; set; }
        public int InvoiceStatusId { get; set; }
        public int? SalesRequestListId { get; set; }
        public int? SparePartTaseerId { get; set; }
        public decimal FixingPrice { get; set; }
        public decimal Deiscount { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }
        public string WarshahId { get; set; }
        public string WarshahName { get; set; }
        public string WarshahCR { get; set; }
        public string WarshahTaxNumber { get; set; }
        public string WarshahPhone { get; set; }
        public string WarshahCity { get; set; }
        public string WarshahDescrit { get; set; }
        public string WarshahStreet { get; set; }
        public string WarshahAddress { get; set; }
        public string WarshahPostCode { get; set; }
        public int? PaymentTypeInvoiceId { get; set; }
        public string PaymentTypeName { get; set; }
        public int? InvoiceCategoryId { get; set; }

    }
}
