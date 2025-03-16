using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class OpenSpartPartDTO : BaseDomainDTO
    {
      
        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int TotalQny { get; set; }

        [Required]
        public decimal TotalMoney { get; set; }
    }
}
