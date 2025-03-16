using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class WarshahDisableReason:BaseDomain
    {
        public int WarshahId { get; set; }
        public string Comment { get; set; }
    }
}
