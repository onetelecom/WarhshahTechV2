using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System.Collections.Generic;

namespace BL.Repositories
{
    public interface IVerfiyCodeRepository
    { }

    public class VerfiyCodeRepository : Repository<VerfiyCode>, IVerfiyCodeRepository
    {
        public VerfiyCodeRepository(AppDBContext ctx) : base(ctx)
        { }

       
    }
}
