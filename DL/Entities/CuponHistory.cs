using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class CuponHistory:BaseDomain
    {
        public decimal BeforeCupon { get; set; }
        public string CopunName { get; set; }
        public string CuponValue { get; set; }
        public decimal AfterCupon { get; set; }
        public int warshahId { get; set; }
    }
}
