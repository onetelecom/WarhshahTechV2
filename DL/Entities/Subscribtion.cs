using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Subscribtion:BaseDomain
    {
        public string SubName { get; set; }
        public int SubDurationInMonths { get; set; }
        public decimal Price { get; set; }
        public bool IsBranch { get; set; }


    }
}
