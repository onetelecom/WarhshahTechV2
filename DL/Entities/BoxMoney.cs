using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class BoxMoney : BaseDomain
    {

      
        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public decimal TotalIncome { get; set; }


    }
}
