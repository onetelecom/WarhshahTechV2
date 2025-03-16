using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class NotificationSetting : BaseDomain
    {
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public int NameNotificationId { get; set; }
        public NameNotification NameNotification { get; set; }

        public int StatusNotificationId { get; set; }

    }
}
