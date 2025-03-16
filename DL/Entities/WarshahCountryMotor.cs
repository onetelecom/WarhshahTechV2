using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class WarshahCountryMotor : BaseDomain
    {
        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int FixedCountryMotorId { get; set; }

        public FixedCountryMotor FixedCountryMotor { get; set; }
    }
}
