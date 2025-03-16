using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class MotorMake : BaseDomain
    {

        [Required]
        [MinLength(1), MaxLength(100)]
        public string MakeNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string MakeNameEn { get; set; }

    }
}
