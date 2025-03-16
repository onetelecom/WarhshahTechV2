using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   public interface IMotorModelRepository
    {
    }


    public class MotorModelRepository : Repository<MotorModel>, IMotorModelRepository
    {
        public MotorModelRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
