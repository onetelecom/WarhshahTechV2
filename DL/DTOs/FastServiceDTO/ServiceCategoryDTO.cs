using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class ServiceCategoryDTO : BaseDomainDTO
    {
        [Required]
        public int WarshahId { get; set; }
        [Required]

        public string  Name { get; set; }

    }
}
