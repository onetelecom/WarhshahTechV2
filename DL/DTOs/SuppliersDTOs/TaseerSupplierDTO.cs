using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SuppliersDTOs
{
    public class TaseerSupplierDTO : BaseDomainDTO
    {


        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }






    }
}
