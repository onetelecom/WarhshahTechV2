using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class MotorModel : BaseDomain
    {

        [Required]
        [MinLength(1), MaxLength(100)]
        public string ModelNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(100)]
        public string ModelNameEn { get; set; }

        // Relationship  ( one MotorMake  to many ModelMotor ) 
        public int MotorMakeId { get; set; }
        public MotorMake MotorMake { get; set; }




    }
}
