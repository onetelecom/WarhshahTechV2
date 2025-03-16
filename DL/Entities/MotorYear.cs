using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public class MotorYear : BaseDomain
    {
        [Required]
        [MinLength(1) , MaxLength(6)]
        public int Year { get; set; }

    }
}
