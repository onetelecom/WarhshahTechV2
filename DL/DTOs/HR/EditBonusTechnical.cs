using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.HR
{
    public class EditBonusTechnical : BaseDomainDTO
    {
        public int Id { get; set; }
        public int WarshahId { get; set; }
        public decimal BonusPercent { get; set; }



    }

}