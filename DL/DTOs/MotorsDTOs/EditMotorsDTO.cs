using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.MotorsDTOs
{
    public class EditMotorsDTO : BaseDomainDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MotorMakeId { get; set; }
        [Required]
        public int MotorModelId { get; set; }
        [Required]
        public int MotorYearId { get; set; }
        //[Required]
        public int MotorColorId { get; set; }
        //[Required]
        [MinLength(1), MaxLength(20)]
        public string ChassisNo { get; set; }
        [Required]
     
        public string PlateNo { get; set; }
        [Required]
        public int CarOwnerId { get; set; }

        public string Type { get; set; }
    }
}
