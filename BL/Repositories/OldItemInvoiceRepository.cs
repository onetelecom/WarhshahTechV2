using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
   
    public interface IOldItemInvoiceRepository
    { }
    public class OldItemInvoiceRepository : Repository<OldInvoiceItem>, IOldItemInvoiceRepository
    {
        public OldItemInvoiceRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
