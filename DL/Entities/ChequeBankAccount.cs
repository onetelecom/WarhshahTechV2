using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ChequeBankAccount : BaseDomain
    {
        public int ChequeId { get; set; }

        public Cheque Cheque { get; set; }

        public int WarshahBankId { get; set; }
        public WarshahBank WarshahBank { get; set; }

        public string Transactiontype { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal CurrentBalance { get; set; }


    }
}
