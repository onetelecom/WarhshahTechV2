using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  
     public interface INotificationRepairOrderRepository
    { }
    public class NotificationRepairOrderRepository : Repository<NotificationRepairOrder>, INotificationRepairOrderRepository
    {
        public NotificationRepairOrderRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
