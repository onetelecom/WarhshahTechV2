using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface ITransactionTodayRepository
    {


    }

    public class TransactionTodayRepository : Repository<TransactionsToday>, ITransactionTodayRepository
    {

        public TransactionTodayRepository(AppDBContext ctx) : base(ctx)
        { }





    }


}
