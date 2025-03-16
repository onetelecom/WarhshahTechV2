using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Notification:BaseDomain
    {
        public int UserId { get; set; }
        public string NotificationText { get; set; }
        public bool Read { get; set; }
    }
}
