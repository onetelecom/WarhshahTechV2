using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs
{
    public class EditNotificationRepairOrderDTO : BaseDomainDTO
    {
        [Required]

        public int Id { get; set; }
        public int WarshahId { get; set; }


        public int NameNotificationId { get; set; }


        public int Minutes { get; set; }

        public int StatusNotificationId { get; set; }

    }
}
