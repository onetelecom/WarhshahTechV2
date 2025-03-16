using DL.DTOs.SharedDTO;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs
{
    public class NotificationRepairOrderAddingDTO : BaseDomainDTO
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

    }
}
