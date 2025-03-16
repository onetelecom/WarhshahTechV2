using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.ExpensesDTOs
{
   public class ExpensesTypeDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(500)]
        public string ExpensesTypeNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(500)]
        public string ExpensesTypeNameEn { get; set; }


        [MinLength(1), MaxLength(20)]
        public string TaxNumber { get; set; }

        [Required]
        public int ExpensesCategoryId { get; set; }
        

        public int? WarshahId { get; set; }
       
    }
}
