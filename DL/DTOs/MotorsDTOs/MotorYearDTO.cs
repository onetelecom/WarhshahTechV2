using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.MotorsDTOs
{
  public  class MotorYearDTO
    {
        [Required]
        
        public int Year { get; set; }

    }
}
