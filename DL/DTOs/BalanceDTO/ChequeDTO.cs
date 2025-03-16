using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using HELPER;
using Helper;

namespace DL.DTOs.BalanceDTO
{
    public class ChequeDTO : BaseDomainDTO
    {
        [Required]
       
        public string ChequeNumber { get; set; }

        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int WarshahBankId { get; set; }

        [Required]
        public int ChequeStatus { get; set; }

        [Required]

        public decimal TotalMoney { get; set; }

        public string MobileClient { get; set; }

        public string PayFor { get; set; }
    }
}
