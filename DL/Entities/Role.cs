using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class Role: BaseDomain
    {
      

        /// <summary>
        /// Role Name 
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(40)]
        public string Name { get; set; }

    }
}
