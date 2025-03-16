using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IInvoiceItemRepository
    { }
public class InvoiceItemRepository : Repository<InvoiceItem>, ICityRepository
{
    public InvoiceItemRepository(AppDBContext ctx) : base(ctx)
    { }


}
}
