using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IMotorsRepository
    { }
    public class MotorsRepository : Repository<Motors>, IMotorsRepository
    {
        public MotorsRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
