using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class CategorySpareParts : BaseDomain
    {
        [Required]
        [MinLength(1), MaxLength(500)]
        public string CategoryNameAr { get; set; }


        [Required]
        [MinLength(1), MaxLength(500)]
        public string CategoryNameEn { get; set; }
    }
}
