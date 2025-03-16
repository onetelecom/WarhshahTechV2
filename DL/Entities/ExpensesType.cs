using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public   class ExpensesType : BaseDomain
    {
        [Required]
        [MinLength(1), MaxLength(500)]
        public string ExpensesTypeNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(500)]
        public string ExpensesTypeNameEn { get; set; }

        [MinLength(1), MaxLength(20)]
        public string TaxNumber { get; set; }

        // Relationship  ( one Category  to many  types ) 
        [Required]
        public int ExpensesCategoryId { get; set; }
        public ExpensesCategory ExpensesCategory { get; set; }

        public int? WarshahId { get; set; }
        public Warshah Warshah { get; set; }
    }
}
