using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.ExpensesDTOs
{
   public class EditExpensesTransactionDTO : BaseDomainDTO
    {

        [Required]
        public int Id { get; set; }



        [Required]
        public int ExpensesCategoryId { get; set; }


        [Required]
        public int ExpensesTypeId { get; set; }

        [Required]
        public string ExpenseNameAr { get; set; }
        public string ExpenseNameEn { get; set; }

   
        public decimal TotalWithoutVat { get; set; }


        public decimal Vat { get; set; }

        [Required]
        public decimal Total { get; set; }

        public string InvoiceNumber { get; set; }

        public int? WarshahId { get; set; }


        [Required]
        public int? PaymentTypeInvoiceId { get; set; }
        public string Notes { get; set; }
    }
}
