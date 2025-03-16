using BL.Infrastructure;
using BL.Repositories;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    
  public interface ISalesInvoiceRepository
    { }

    public class SalesInvoiceRepository : Repository<SalesInvoice>, ISalesInvoiceRepository
{

        public SalesInvoiceRepository(AppDBContext ctx) : base(ctx)
        { }





    }
}
