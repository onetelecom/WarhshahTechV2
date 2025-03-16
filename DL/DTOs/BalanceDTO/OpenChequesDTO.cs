using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class OpenChequesDTO : BaseDomainDTO
    {
        [Required]
        public int WarshahId { get; set; }


        [Required]
        public decimal TotalMoney { get; set; }

    }
}
