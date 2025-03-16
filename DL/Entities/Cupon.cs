using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class Cupon:BaseDomain
    {
     
        public int Value { get; set; }
        public string CuponName { get; set; }
        public int SubId { get; set; }
    }
}
