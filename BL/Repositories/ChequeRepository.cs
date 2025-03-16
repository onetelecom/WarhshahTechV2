using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IChequeRepository
    { }
    public class ChequeRepository : Repository<Cheque>, IChequeRepository
    { 
        public ChequeRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
