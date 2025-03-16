using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    

    public interface ISparePartTaseerRepository
    { }
    public class SparePartTaseerRepository : Repository<SparePartTaseer>, ISparePartTaseerRepository
    {
        public SparePartTaseerRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
