using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.BalanceDTO
{
    public class EditOpenBalanceDTO : BaseDomainDTO

    {

        [Required]
        public int Id { get; set; }

        [Required]
        public int WarshahId { get; set; }
        public decimal OpenBalance { get; set; }

    }

}
