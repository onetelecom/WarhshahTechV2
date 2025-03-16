using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface ICityRepository
    { }
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
