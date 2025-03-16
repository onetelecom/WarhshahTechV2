using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface ISalesInvoiceItemRepository
    { }

    public class SalesInvoiceItemRepository : Repository<SalesInvoiceItem>, ISalesInvoiceItemRepository
    {
        public SalesInvoiceItemRepository(AppDBContext ctx) : base(ctx)
        { }

    }
    }



