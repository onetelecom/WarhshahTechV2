using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.UserDTOs
{
    public class WarshahFixedTypeDTO : BaseDomainDTO
    {
        [Required]
        public string NameType { get; set; }
    }
}
