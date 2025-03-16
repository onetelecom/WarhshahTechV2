using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class FixedCountryMotor : BaseDomain
    {
        [Required]
        public string CountryMotorName { get; set; }


    }
}
