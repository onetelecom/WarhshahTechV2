using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IBoxRepository
    { }
    public class BoxRepository : Repository<BoxMoney>, IBoxRepository
    {
        public BoxRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
