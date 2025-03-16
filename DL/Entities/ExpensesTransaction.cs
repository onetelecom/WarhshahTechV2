using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public  class ExpensesTransaction : BaseDomain
    {
        [Required]       
        public int ExpensesCategoryId { get; set; }
        public ExpensesCategory ExpensesCategory { get; set; }

        [Required]
        public int ExpensesTypeId { get; set; }
        public ExpensesType ExpensesType { get; set; }

      
        public decimal TotalWithoutVat { get; set; }

       [Required]
        public string ExpenseNameAr { get; set; }
        public string ExpenseNameEn { get; set; }

        public decimal Vat { get; set; }

        [Required]
        public decimal Total { get; set; }

        public string InvoiceNumber { get; set; }

        public int? WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
        public int? PaymentTypeInvoiceId { get; set; }
        public virtual PaymentTypeInvoice PaymentTypeInvoice { get; set; }
        public string Notes { get; set; }
    }
}
