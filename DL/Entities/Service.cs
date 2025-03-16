using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class Service:BaseDomain
    {
        public int WarshahId { get; set; }
        public int ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
