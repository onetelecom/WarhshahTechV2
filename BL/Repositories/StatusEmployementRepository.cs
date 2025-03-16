using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IStatusEmployementRepository
    { }
    public class StatusEmployementRepository : Repository<StatusEmployment>, IStatusEmployementRepository
    {
        public StatusEmployementRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
