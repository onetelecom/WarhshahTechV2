using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class IncomeAndVatReport
    {

        public decimal TotalSales { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal TotalExpenses { get; set; }

        public decimal TotalExpensesWithoutVat { get; set; }
        public decimal VatSales { get; set; }
        public decimal VatPurchases { get; set; }
        public decimal VatExpenses { get; set; }

        public decimal ZeroVatExpenses { get; set; }
        public decimal NetIncome { get; set; }
        public decimal NetVat { get; set; }


    }
}
