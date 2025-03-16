using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class EditBalanceBankDTO
    {

        [Required]
        public int Id { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        public int WarshahId { get; set; }


    }
}
