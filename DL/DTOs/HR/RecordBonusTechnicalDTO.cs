using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.HR
{
    public class RecordBonusTechnicalDTO : BaseDomainDTO
    {

        [Required]
        public int UserId { get; set; }
      

        public decimal? Bonus { get; set; }


    }
}