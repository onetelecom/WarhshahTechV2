using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DTO
{
   public class UserRegisterDTO
    {
        [MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(50)]
        public string LastName { get; set; }

        [Required]

        public string MobileNum { get; set; }
      

        public string Email { get; set; }
        [Required]
      
        public string Password { get; set; }
       

        

    }
}
