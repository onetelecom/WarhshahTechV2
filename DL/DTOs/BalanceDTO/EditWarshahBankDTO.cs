using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class EditWarshahBankDTO  : BaseDomainDTO
    {

        [Required]

        public int Id { get; set; }


        [Required]
        public int FixedBankId { get; set; }

       
        [MinLength(1), MaxLength(24)]
        public string IPAN { get; set; }


        public string AccountName { get; set; }

        public string BankAccountNumber { get; set; }

        [Required]
        public int WarshahId { get; set; }



        public decimal OpenBalance { get; set; }



        public decimal CurrentBalance { get; set; }

    }
}
