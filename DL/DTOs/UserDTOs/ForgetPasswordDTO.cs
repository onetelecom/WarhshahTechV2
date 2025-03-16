using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DTOs.UserDTOs
{
    public class ForgetPasswordDTO
    {
        public string EncId { get; set; }
        public string NewPassword { get; set; }
    }
}
