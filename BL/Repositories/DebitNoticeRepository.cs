using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  
    public interface IDebitNoticeRepository
    { }

    public class DebitNoticeRepository : Repository<DebitNotice>, IDebitNoticeRepository
    {
        public DebitNoticeRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
