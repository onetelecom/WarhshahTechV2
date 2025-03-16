using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class TransferBankAccount : BaseDomain
    {
        public int TransferId { get; set; }

        public Transfer Transfer { get; set; }

        public int WarshahBankId { get; set; }
        public WarshahBank WarshahBank { get; set; }

        public string Transactiontype { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal CurrentBalance { get; set; }


    }
}
