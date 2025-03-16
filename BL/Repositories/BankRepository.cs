using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IBankRepository
    { }
    public class BankRepository : Repository<Bank>, IBankRepository
    {
        public BankRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
