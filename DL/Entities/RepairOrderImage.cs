using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class RepairOrderImage:BaseDomain
    {
        public int RepairOrderId { get; set; }
        public string ImageName { get; set; }
    }
}
