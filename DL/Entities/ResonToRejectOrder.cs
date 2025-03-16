using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ResonToRejectOrder:BaseDomain
    {
        public int OrderId { get; set; }
        public string Reson { get; set; }
    }
}
