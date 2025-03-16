using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IOpeningBalanceRepository
    {


    }

    public class OpeningBalanceRepository : Repository<OpeningBalance>, IOpeningBalanceRepository
    {

        public OpeningBalanceRepository(AppDBContext ctx) : base(ctx)
        { }





    }


}
