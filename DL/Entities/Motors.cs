using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
 public  class Motors : BaseDomain
    {
        [Required]
        public int MotorMakeId { get; set; }
        public MotorMake motorMake { get; set; }
        [Required]
        public int MotorModelId { get; set; }
        public MotorModel motorModel { get; set; }
        [Required]
        public int MotorYearId { get; set; }
        public MotorYear motorYear { get; set; }
        [Required]
        public int MotorColorId { get; set; }
        public MotorColor motorColor { get; set; }
       
        public string ChassisNo { get; set; }
        [Required]
       
        public string PlateNo { get; set; }
        [Required]
        public int CarOwnerId { get; set; }
        public User CarOwner{ get; set; }

        public string Type { get; set; }

    }
}
