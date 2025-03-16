using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System.Collections.Generic;

namespace BL.Repositories
{
    public interface IRoleRepository
    { }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDBContext ctx) : base(ctx)
        { }

       
    }
}
