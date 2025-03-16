using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.UserDTOs
{
    public class WarshahModelCarDTO : BaseDomainDTO
    {
        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int MotorModelId { get; set; }


    }
}
