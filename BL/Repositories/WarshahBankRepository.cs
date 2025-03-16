using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IWarshahBankRepository
    { }
    public class WarshahBankRepository : Repository<WarshahBank>, IWarshahBankRepository
    {
        public WarshahBankRepository(AppDBContext ctx) : base(ctx)
        { }


    }





}
