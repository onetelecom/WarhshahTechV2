using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class WarshahType:BaseDomain
    {
        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int WarshahFixedTypeId { get; set; }

        public WarshahFixedType WarshahFixedType { get; set; }

    }
}
