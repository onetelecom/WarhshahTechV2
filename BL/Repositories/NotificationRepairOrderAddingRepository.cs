using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface INotificationRepairOrderAddingRepository
    { }
    public class NotificationRepairOrderAddingRepository : Repository<NotificationRepairOrderAdding>, INotificationRepairOrderAddingRepository
    {
        public NotificationRepairOrderAddingRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
