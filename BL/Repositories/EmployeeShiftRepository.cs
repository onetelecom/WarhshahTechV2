using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IEmployeeShiftRepository
    { }
    public class EmployeeShiftRepository : Repository<EmployeeShift>, IEmployeeShiftRepository
    {
        public EmployeeShiftRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
