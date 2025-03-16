using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class EditBankDTO
    {


        [Required]

        public int Id { get; set; }

       
        [Required]
        public int WarshahId { get; set; }

        [Required]

        public decimal CurrentBalance { get; set; }
    }
}
