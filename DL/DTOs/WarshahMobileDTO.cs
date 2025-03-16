using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs
{
   public class WarshahMobileDTO
    {
        [Required]
        [MinLength(1), MaxLength(100)]
        public string Mobile { get; set; }

        public int WarshahId { get; set; }
    }
}
