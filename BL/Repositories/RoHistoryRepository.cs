using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface IRoHistoryRepository
    { }
    public class RoHistoryRepository : Repository<RoHistory>, IRoHistoryRepository
    {
        public RoHistoryRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
