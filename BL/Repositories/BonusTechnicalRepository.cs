using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
     public interface IBonusTechnicalRepository
    { }
    public class BonusTechnicalRepository : Repository<BonusTechnical>, IBonusTechnicalRepository
    {
        public BonusTechnicalRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
