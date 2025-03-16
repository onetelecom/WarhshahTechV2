using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public  class permission:BaseDomain
    {
        [Required]
        public string Name { get; set; }
    }
}
