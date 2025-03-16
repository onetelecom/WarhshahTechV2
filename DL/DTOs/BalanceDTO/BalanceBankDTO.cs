using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class BalanceBankDTO : BaseDomainDTO
    {
        [Required]
        public decimal OpeningBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        [Required]
        public int WarshahId { get; set; }
   

    }
}
