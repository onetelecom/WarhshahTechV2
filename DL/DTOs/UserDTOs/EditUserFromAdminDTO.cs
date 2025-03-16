using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.UserDTOs
{
    public class EditUserFromAdminDTO : BaseDomainDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// User Nae
        /// </summary>
        [MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(50)]
        public string LastName { get; set; }

        public string Phone { get; set; }

    }
}
