using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class MotorColor : BaseDomain
    {
        [Required]
        [MinLength(1), MaxLength(100)]
        public string ColorNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string ColorNameEn { get; set; }

    }
}
