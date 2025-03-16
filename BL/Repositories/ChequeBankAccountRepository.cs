using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IChequeBankAccountRepository
    { }
    public class ChequeBankAccountRepository : Repository<ChequeBankAccount>, IChequeBankAccountRepository
    {
        public ChequeBankAccountRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
