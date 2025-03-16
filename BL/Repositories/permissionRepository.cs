using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System.Collections.Generic;

namespace BL.Repositories
{
    public interface IpermissionRepository
    { }

    public class permissionRepository : Repository<permission>, IpermissionRepository
    {
        public permissionRepository(AppDBContext ctx) : base(ctx)
        { }

       
    }
}
