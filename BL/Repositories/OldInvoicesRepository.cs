using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
 
    public interface IOldInvoicesRepository
    { }
    public class OldInvoicesRepository : Repository<OldInvoice>, IOldInvoicesRepository
    {
        public OldInvoicesRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
