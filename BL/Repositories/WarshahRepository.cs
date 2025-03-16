using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System.Collections.Generic;

namespace BL.Repositories
{
    public interface IWarshahRepository
    { }

    public class WarshahRepository : Repository<Warshah>, IWarshahRepository
    {
        public WarshahRepository(AppDBContext ctx) : base(ctx)
        { }

       
    }
}
