using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class ServiceInvoiceItem:BaseDomain
    {
        public int ServiceInvoiceId { get; set; }
        public ServiceInvoice ServiceInvoice { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }


    }
}
