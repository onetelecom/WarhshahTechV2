using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IExpensesTypeRepository
    { }
    public class ExpensesTypeRepository : Repository<ExpensesType>, IExpensesTypeRepository
    {
        public ExpensesTypeRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
