using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface IWarshahFixedTypeRepository
    { }
    public class WarshahFixedTypeRepository : Repository<WarshahFixedType>, IWarshahFixedTypeRepository
    {
        public WarshahFixedTypeRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
