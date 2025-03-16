using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    

    public interface ISparePartRepository
    { }
    public class SparePartRepository : Repository<SparePart>, ISparePartRepository
    {
        public SparePartRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
