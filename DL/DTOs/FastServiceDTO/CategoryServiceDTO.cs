using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.FastServiceDTO
{
   public class CategoryServiceDTO
    {
        public string CategoryName { get; set; }
        public List<ServiceInvoiceItemDTO> serviceInvoiceItemDTOs { get; set; }
    }
}
