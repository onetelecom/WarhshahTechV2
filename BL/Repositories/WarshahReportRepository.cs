using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  
    public interface IWarshahReportRepository
    { }

    public class WarshahReportRepository : Repository<WarshahReport>, IWarshahReportRepository
    {
        public WarshahReportRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
