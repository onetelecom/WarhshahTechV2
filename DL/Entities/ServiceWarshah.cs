using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class ServiceWarshah : BaseDomain
    {
        [Required]
        public int WarshahId { get; set; }

        [Required]
        public int warshahServiceTypeID { get; set; }

        public WarshahServiceType warshahServiceType { get; set; }

    }
}
