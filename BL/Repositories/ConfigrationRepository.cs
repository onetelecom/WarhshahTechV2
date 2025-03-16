using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface IConfigrationRepository
    { }
    public class ConfigrationRepository : Repository<Configration>, IConfigrationRepository
    {
        public ConfigrationRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
