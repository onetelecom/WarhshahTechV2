using BL.Helpers;
using BL.Infrastructure;
using DL.DBContext;

using DL.DTOs.SharedDTO;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public interface ITaseerSupplierRepository
    {
     

    }

    public class TaseerSupplierRepository : Repository<TaseerSupplier>, ITaseerSupplierRepository
    {

        public TaseerSupplierRepository(AppDBContext ctx) : base(ctx)
        { }


       


    }
}
