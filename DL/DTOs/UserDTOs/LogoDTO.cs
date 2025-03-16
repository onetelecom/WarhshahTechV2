using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.UserDTOs
{
    public class LogoDTO
    {
        public int WarshahId { get; set; }
        public IFormFile Logo { get; set; }
    }
}
