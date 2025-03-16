using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IExpensesCategoryRepository
    {


    }

    public class ExpensesCategoryRepository : Repository<ExpensesCategory>, IExpensesCategoryRepository
    {

        public ExpensesCategoryRepository(AppDBContext ctx) : base(ctx)
        { }





    }

}
