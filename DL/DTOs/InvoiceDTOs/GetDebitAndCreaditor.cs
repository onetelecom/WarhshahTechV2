using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
    public class GetDebitAndCreaditor
    {
        public DebitAndCreditor debitAndCreditorDTO { get; set; }

        public HashSet<NoticeProduct> noticeProductDTOs { get; set; }

    }
}
