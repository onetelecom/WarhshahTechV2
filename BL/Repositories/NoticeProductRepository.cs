using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface INoticeProductRepository
    {


    }

    public class NoticeProductRepository : Repository<NoticeProduct>, INoticeProductRepository
    {

        public NoticeProductRepository(AppDBContext ctx) : base(ctx)
        { }





    }
}
