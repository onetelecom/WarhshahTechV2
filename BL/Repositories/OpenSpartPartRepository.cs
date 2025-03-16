using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IOpenSpartPartRepository { }
    public class OpenSpartPartRepository : Repository<OpenSpartPart>, IOpenSpartPartRepository
    {
        public OpenSpartPartRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
