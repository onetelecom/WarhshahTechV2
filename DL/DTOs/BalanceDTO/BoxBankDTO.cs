using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class BoxBankDTO : BaseDomainDTO
    {
        [Required]
        public int WarshahId { get; set; }
      

        public int WarshahBankId { get; set; }
        public int UserId { get; set; }


        public string NoDispote { get; set; }




        public decimal TotalIncome { get; set; }
    }
}
