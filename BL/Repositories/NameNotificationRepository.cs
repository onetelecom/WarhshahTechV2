using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    

   public interface INameNotificationRepository
    { }
    public class NameNotificationRepository : Repository<NameNotification>, INameNotificationRepository
    {
        public NameNotificationRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
