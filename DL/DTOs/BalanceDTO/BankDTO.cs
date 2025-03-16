using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class BankDTO : BaseDomainDTO
    {

        [Required]
        public int WarshahId { get; set; }

        [Required]
        public decimal CurrentBalance { get; set; }

    }
}
