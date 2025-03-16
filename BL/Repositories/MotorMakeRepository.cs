using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  

    public interface IMotorMakeRepository
    { }

    public class MotorMakeRepository : Repository<MotorMake>, IMotorMakeRepository
    {
        public MotorMakeRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
