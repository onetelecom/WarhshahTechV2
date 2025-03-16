using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class WarshahParams:BaseDomain
    {
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }
        public int WarshahServiceTypeId { get; set; }
        public WarshahServiceType WarshahServiceType { get; set; }
        public int SpicialistsId { get; set; }
        public Spicialists Spicialists { get; set; }
        public int WarshahTypeId { get; set; }
        public WarshahType WarshahType { get; set; }
    } 
}
