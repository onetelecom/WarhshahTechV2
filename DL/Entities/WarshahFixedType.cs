using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class WarshahFixedType : BaseDomain
    {
        [Required]
        public string NameType { get; set; }

    }
}
