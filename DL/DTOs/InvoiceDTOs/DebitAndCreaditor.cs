using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
   public class DebitAndCreaditor
    {
        public DebitAndCreditorDTO debitAndCreditorDTO { get; set; }

        public HashSet<NoticeProductDTO> noticeProductDTOs { get; set; }
    }
}
