using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    
    public interface ICountryRepository
    { }

    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
