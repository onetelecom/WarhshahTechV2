using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.ExpensesDTOs
{
  public  class ExpensesCategoryDTO : BaseDomainDTO
    {
        [Required]
        [MinLength(1), MaxLength(500)]
        public string ExpensesCategoryNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(500)]
        public string ExpensesCategoryNameEn { get; set; }
    }
}
