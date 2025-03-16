using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class TransferDTO : BaseDomainDTO
    {
        [Required]
        public string TransferNumber { get; set; }

        [Required]
        public int WarshahId { get; set; }
    

        [Required]
        public int WarshahBankId { get; set; }
   

        [Required]
        public int TransferStatus { get; set; }

        [Required]

        public decimal TotalMoney { get; set; }

        public string MobileClient { get; set; }

        public string PayFor { get; set; }


    }
}
