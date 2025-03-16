using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities.HR
{
    public class BoxBank : BaseDomain
    {

        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public int WarshahBankId { get; set; }
        public WarshahBank WarshahBank { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string NoDispote { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
