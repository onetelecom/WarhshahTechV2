using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    
     public interface IMartialStatusRepository
    { }
    public class MartialStatusRepository : Repository<MaritalStatus>, IMartialStatusRepository
    {
        public MartialStatusRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
