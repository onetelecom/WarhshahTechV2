using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public  class ServiceDTO : BaseDomainDTO
    {
        [Required]
        public int WarshahId { get; set; }
        [Required]
        public int ServiceCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

    }
}
