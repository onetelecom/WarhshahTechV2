using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class EditChequeBankAccountDTO : BaseDomainDTO
    {

        public int Id { get; set; }
        public int ChequeId { get; set; }

        public string Transactiontype { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal CurrentBalance { get; set; }

    }
}
