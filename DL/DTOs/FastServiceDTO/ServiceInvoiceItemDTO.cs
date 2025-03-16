using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class ServiceInvoiceItemDTO : BaseDomainDTO
    {
        public int? ServiceInvoiceId { get; set; }

        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }

    }
}
