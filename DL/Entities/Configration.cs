using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
   public class Configration:BaseDomain
    {
        public int WarshahId { get; set; }
        public bool GetCustomerAbroval { get; set; }
        public int PeriodDayCustomerApprove { get; set; }

        public decimal GetVAT { get; set; }
    }
}
