using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs
{
    public class EditNotificationSettingDTO
    {
        [Required]

        public int Id { get; set; }
        public int WarshahId { get; set; }



        public int NameNotificationId { get; set; }


        public int StatusNotificationId { get; set; }

    }
}
