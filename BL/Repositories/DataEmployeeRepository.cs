using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   public interface IDataEmployeeRepository
    { }
    public class DataEmployeeRepository : Repository<DataEmployee>, IDataEmployeeRepository
    {
        public DataEmployeeRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}