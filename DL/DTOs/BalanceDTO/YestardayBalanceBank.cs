using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class YestardayBalanceBank
    {

        public string BankName { get; set; }

        public string AccountName { get; set; }

        public string BankAccountNumber { get; set; }

        public decimal YestardayBalance { get; set; }




    }
}
