using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IMotorYearRepository
    { }
    public class MotorYearRepository : Repository<MotorYear>, IMotorYearRepository
    {
        public MotorYearRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
