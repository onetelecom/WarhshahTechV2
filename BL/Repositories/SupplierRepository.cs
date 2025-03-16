using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface ISupplierRepository
    { }
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
