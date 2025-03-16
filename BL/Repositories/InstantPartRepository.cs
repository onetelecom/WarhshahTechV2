using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IInstantPartRepository
    { }
    public class InstantPartRepository : Repository<InstantPart>, IInstantPartRepository
    {
        public InstantPartRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}