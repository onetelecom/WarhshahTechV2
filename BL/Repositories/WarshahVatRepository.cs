using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{


    public interface IWarshahVatRepository
    { }

    public class WarshahVatRepository : Repository<WarshahVat>, IWarshahVatRepository
    { 
        public WarshahVatRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
