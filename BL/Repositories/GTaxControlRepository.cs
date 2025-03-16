using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IGTaxControlRepository
    { }
    public class GTaxControlRepository : Repository<GTaxControl>, IGTaxControlRepository
    {
        public GTaxControlRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
