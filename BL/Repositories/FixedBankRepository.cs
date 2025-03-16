using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IFixedBankRepository
    {


    }

    public class FixedBankRepository : Repository<FixedBank>, IFixedBankRepository
    {

        public FixedBankRepository(AppDBContext ctx) : base(ctx)
        { }





    }

}
