using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.InvoiceDTOs
{
   public class EditCreaditorNoticeDTO
    {



        [Required]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        public int WarshahId { get; set; }
       
        [Required]
        public decimal ReturnMoney { get; set; }
        public decimal ReturnVat { get; set; }
        public decimal Total { get; set; }

    }
}
