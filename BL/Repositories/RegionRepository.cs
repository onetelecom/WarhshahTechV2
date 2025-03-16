using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   

    public interface IRegionRepository
    { }
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
