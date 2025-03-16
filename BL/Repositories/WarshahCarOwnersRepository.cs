using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

   public interface IWarshahCarOwnersRepository
    { }
    public class WarshahCarOwnersRepository : Repository<WarshahWithCarOwner>, IWarshahCarOwnersRepository
    {
        public WarshahCarOwnersRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
