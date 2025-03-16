using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IOpenBankBalanceRepository
    { }

    public class OpenBankBalanceRepository : Repository<OpenBalanceBank>, IOpenBankBalanceRepository
    {
        public OpenBankBalanceRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
