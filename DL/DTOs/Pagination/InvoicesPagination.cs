using DL.DTOs.InvoiceDTOs;
using DL.DTOs.JobCardDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.Pagination
{
    public class InvoicesPagination
    {
        public List<InvocieCreditDTO> Invoices { get; set; } = new List<InvocieCreditDTO>();
        public int Pages { get; set; }

        public int CurrentPage { get; set; }



    }
}
