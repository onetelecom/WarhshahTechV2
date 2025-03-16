using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class WarshahModelsCar : BaseDomain
    {

        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int MotorModelId { get; set; }

        public MotorModel MotorModel { get; set; }


    }
}
