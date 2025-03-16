using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class BalanceBank : BaseDomain
    {

        [Required]
        public decimal OpeningBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

    }
}
