using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class OpenSpartPart : BaseDomain
    {
       
        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
        public int TotalQny { get; set; }

        [Required]
        public decimal TotalMoney { get; set; }

    }
}
