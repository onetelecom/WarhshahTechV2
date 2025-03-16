using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class Cheque : BaseDomain
    {
        [Required]
        public string ChequeNumber { get; set; }

        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
        public int WarshahBankId { get; set; }
        public WarshahBank WarshahBank { get; set; }


        [Required]
        public int ChequeStatus { get; set; }

        [Required]

        public decimal TotalMoney { get; set; }

        public string MobileClient { get; set; }

        public string PayFor { get; set; }


    }
}
