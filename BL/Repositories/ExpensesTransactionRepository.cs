using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IExpensesTransactionRepository
    { }

    public class ExpensesTransactionRepository : Repository<ExpensesTransaction>, IExpensesTransactionRepository
    {
        public ExpensesTransactionRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
