using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class CalculateCheque
    {
        public decimal TotalCheques { get; set; }
        public decimal DiposteCheques { get; set; }
        public decimal WatingCheques { get; set; }

    }
}
