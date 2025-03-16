using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.MotorsDTOs
{
  public  class EditMotorYearDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
       
        public int Year { get; set; }

    }
}
