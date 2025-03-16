using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class NotificationRepairOrder : BaseDomain
    {
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public int NameNotificationId { get; set; }
        public NotificationRepairOrderAdding NameNotification { get; set; }

        public int Minutes { get; set; }

        public int StatusNotificationId { get; set; }

    }
}
