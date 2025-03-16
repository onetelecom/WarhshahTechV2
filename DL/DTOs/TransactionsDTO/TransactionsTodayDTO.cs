using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.TransactionsDTO
{
    public class TransactionsTodayDTO : BaseDomainDTO
    {

        public int WarshahId { get; set; }
        public int TransactionTypeID { get; set; }

        public int InvoiceNumber { get; set; }

        public string TransactionName { get; set; }

        public string TransactionReason { get; set; }
        public decimal BeforeVat { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal CurrentBalance { get; set; }

    }
}
