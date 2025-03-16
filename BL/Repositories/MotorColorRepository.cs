using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IMotorColorRepository
    { }

    public class MotorColorRepository : Repository<MotorColor>, IMotorColorRepository
    {
        public MotorColorRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
