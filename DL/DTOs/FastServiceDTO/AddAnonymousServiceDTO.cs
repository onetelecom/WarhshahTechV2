using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.FastServiceDTO
{
   public class AddAnonymousServiceDTO
    {
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string CategoryId { get; set; }
        public int WarshahId { get; set; }
    }
}
