using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface IOpenCheaqeRepository
    { }

    public class OpenCheaqeRepository : Repository<OpenBalanceCheque>, IOpenCheaqeRepository
    {
        public OpenCheaqeRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
