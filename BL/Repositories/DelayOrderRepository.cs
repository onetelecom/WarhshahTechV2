using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

      public interface IDelayOrderRepository
    { }

    public class DelayOrderRepository : Repository<DelayRepairOrder>, IDelayOrderRepository
    {
        public DelayOrderRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
