using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{


    public interface INotificationSettingRepository { }
    public class NotificationSettingRepository : Repository<NotificationSetting>, INotificationSettingRepository
    {
        public NotificationSettingRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
