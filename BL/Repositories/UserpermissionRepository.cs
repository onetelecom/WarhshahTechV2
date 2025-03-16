using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System.Collections.Generic;

namespace BL.Repositories
{
    public interface IUserpermissionRepository
    { }

    public class UserpermissionRepository : Repository<UserPermission>, IUserpermissionRepository
    {
        public UserpermissionRepository(AppDBContext ctx) : base(ctx)
        { }

       
    }
}
