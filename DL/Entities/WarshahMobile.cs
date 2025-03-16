using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class WarshahMobile : BaseDomain
    {

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Mobile { get; set; }

        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }
    }
}
