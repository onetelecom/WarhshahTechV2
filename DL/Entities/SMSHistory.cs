using DL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class SMSHistory:BaseDomain
    {
        public string SMSMessage { get; set; }
        public string UserName { get; set; }
        public int WarshahId { get; set; }
    }
}
