using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class WarshahBank : BaseDomain
    {

        [Required]
        public int FixedBankId { get; set; }

        public FixedBank FixedBank { get; set; }

        public string AccountName { get; set; }

       
        [MinLength(1), MaxLength(24)]
        public string IPAN { get; set; }
        public string BankAccountNumber { get; set; }

        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]

        public decimal OpenBalance { get; set; }



        public decimal CurrentBalance { get; set; }


       



    }
}
