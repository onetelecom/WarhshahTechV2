using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class ChequeBankAccountDTO : BaseDomainDTO
    {
        public int ChequeId { get; set; }

        public int WarshahBankId { get; set; }
     

        public string Transactiontype { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal CurrentBalance { get; set; }

    }
}
