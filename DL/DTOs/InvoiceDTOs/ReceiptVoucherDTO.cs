using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
   public class ReceiptVoucherDTO
    {

        
       
        [Required]
        public int DocNumber { get; set; }
        [Required]
        public int ReciptionOrderId { get; set; }
        public int WarshahId { get; set; }
        

        [Required]
        public decimal AdvancePayment { get; set; }
        [Required]
        public int PaymentTypeInvoiceId { get; set; }
        
        [Required]
        public string Discriptotion { get; set; }
        

    }
}
